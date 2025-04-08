using Mirror;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateGamePanel : MonoBehaviour
{
   #region Fields & Properties

   //[SerializeField] GameObject landingPange;
   [SerializeField] TMP_Text msgText;
   [SerializeField] TMP_InputField nopInput;
   [SerializeField] Toggle nwGameToggle, timedGameToggle;
   [SerializeField] TMP_InputField nwAmountInput, timedLengthInput;
   [SerializeField] Toggle borgDieToggle;
   [SerializeField] Button ConfirmButton, backButton;

   //public static Action<IFG.GameType, int, float, bool> GameInfoSet; //gameType,nwAmount,tgLength,borgDie

   [field: SerializeField] public static int nwAmount;
   [field: SerializeField] public static IFG.GameType gameType;
   [field: SerializeField] public static float tgLength;
   [field: SerializeField] public static bool borg = true;

   MyNetworkManager room;
   MyNetworkManager Room
   {
      get
      {
         if (room != null) return room;

         return room = NetworkManager.singleton as MyNetworkManager;
      }
   }

   #endregion

   #region Unity Callbacks

   //void Awake() 
   //{
   //	
   //}

   void Start()
   {
      nopInput.Select();
   }

   //void Update()
   //{
   //   
   //}

   #endregion

   public void OnNopInputValueChanged(string value)
   {
      Room.SetMinPlayers(int.Parse(value));
      nwGameToggle.isOn = true;
      nwAmountInput.gameObject.SetActive(true);
      nwAmountInput.Select();
   }

   public void OnNwGameToggleValueChanged(bool value)
   {
      timedGameToggle.gameObject.SetActive(value ? false : true);
      nwAmountInput.gameObject.SetActive(value ? true : false);
      if (value)
      {
         gameType = (int)IFG.GameType.NETWORTH;
         nwAmountInput.Select();
      }
   }

   public void OnTimedGameToggleValueChanged(bool value)
   {
      nwGameToggle.gameObject .SetActive(value ? false : true);
      timedLengthInput.gameObject.SetActive(value ? true : false);
      if (value)
      {
         gameType = IFG.GameType.TIMED;
         timedLengthInput.Select();
      }
   }

   public void OnNwAmtInputValueChanged(string value)
   {
      nwAmount = int.Parse(value);
      ConfirmButton.interactable = true;
   }

   public void OnTgLengthInputValueChanged(string value)
   {
      tgLength = float.Parse(value);
      ConfirmButton.interactable = true;
   }

   public void OnBorgDieToggleValueChanged(bool value)
   {
      borg = value;
   }

   public void OnConfirmButtonClick()
   {
      //GameInfoSet?.Invoke(gameType, nwAmount, tgLength, borg);
      Room.GameType = gameType;
      Room.NetworthAmount = nwAmount;
      Room.TimedLength = tgLength;
      Room.BorgDie = borg;
      gameObject.SetActive(false);
      Room.StartHost();
   }
}
