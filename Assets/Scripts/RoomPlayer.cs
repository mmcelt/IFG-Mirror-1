using Mirror;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomPlayer : NetworkBehaviour
{
   #region Fields & Properties
   [Header("UI")]
   [SerializeField] GameObject lobbyUI;
   [SerializeField] TMP_Text[] playerNameTexts;
   [SerializeField] TMP_Text[] playerReadyTexts;
   [SerializeField] Button startGameButton;
   [SerializeField] Transform farmerSelectionButtonsHolder;
   [SerializeField] FarmerSelectButton farmerSelectionButtonPrefab;
   [SerializeField] FarmerDatabaseSO farmerDatabase;
   [SerializeField] FarmerSO selectedFarmer; //SET BY SELECTION BUTTON
   public Button lockInButton;
   [Header("Intro Tractor Stuff")]
   public Texture[] IntroTextures;
   public GameObject IntroPrefab, IntroInstance;
   public Transform IntroSpawnPoint;

   [SyncVar(hook = nameof(HandleDisplayNameChanged))]
   public string DisplayName = "Loading...";
   [SyncVar(hook = nameof(HandleFarmerNameChanged))]
   public string FarmerName = "Not Set";
   [SyncVar(hook = nameof(HandleReadyStatusChanged))]
   public bool IsReady = false;

   bool isLeader;
   public bool IsSpawnedIn;

   public bool IsLeader
   {
      set
      {
         isLeader = value;
         startGameButton.gameObject.SetActive(value);
      }
   }

   //game info...
   [SerializeField] int nop;
   //[SerializeField] IFG.GameType gameType;
   //[SerializeField] int networthAmount;
   //[SerializeField] float timedLength;
   //[SerializeField] bool borgDie;

   MyNetworkManager room;

   public MyNetworkManager Room
   {
      get
      {
         if (room != null) return room;
         return room = NetworkManager.singleton as MyNetworkManager;
      }
   }

   [SerializeField] List<FarmerSelectButton> farmerSelectButtons = new List<FarmerSelectButton>();

   #endregion

   #region Setters & Getters

   public void SetNOP(int _nop)
   {
      nop = _nop;
   }

   //public void SetGameType(IFG.GameType _gameType)
   //{
   //   gameType = _gameType;
   //}

   //public void SetNetworthAmount(int _networthAmount)
   //{
   //   networthAmount = _networthAmount;
   //}

   //public void SetTimedLength(float _timedLength)
   //{
   //   timedLength = _timedLength;
   //}

   //public void SetBorgDie(bool _borgDie)
   //{
   //   borgDie = _borgDie;
   //}

   public int GetNOP()
   {
      return nop;
   }

   //public IFG.GameType GetGameType()
   //{
   //   return gameType;
   //}

   //public int GetNetworthAmount()
   //{
   //   return networthAmount;
   //}

   //public float GetTimedLength()
   //{
   //   return timedLength;
   //}

   //public bool GetBorgDie()
   //{
   //   return borgDie;
   //}

   #endregion

   #region Mirror Callbacks

   public override void OnStartAuthority()
   {
      CmdSetDisplayName(PlayerNameInput.DisplayName);
      lobbyUI.SetActive(true);

      CreateFarmerSelectionButtons();
   }

   public override void OnStartClient()
   {
      Room.RoomPlayers.Add(this);
      UpdateDisplay();
   }

   public override void OnStopClient()
   {
      Room.RoomPlayers.Remove(this);
      UpdateDisplay();
   }
   #endregion

   #region Client

   void CreateFarmerSelectionButtons()
   {
      foreach (var farmer in farmerDatabase.Farmers)
      {
         var selectionButtonInstance = Instantiate(farmerSelectionButtonPrefab, farmerSelectionButtonsHolder);
         selectionButtonInstance.SetupButton(this, farmer);
         farmerSelectButtons.Add(selectionButtonInstance);
      }

      //Transform[] myChildren=farmerSelectionButtonsHolder.GetComponentsInChildren<Transform>();
      //Debug.Log($"My Children: {myChildren.Length}");
   }

   public void SetSelectedFarmer(FarmerSO farmer)
   {
      selectedFarmer = farmer;
   }

   public void HandleDisplayNameChanged(string oldValue, string newValue)
   {
      UpdateDisplay();
   }

   public void HandleFarmerNameChanged(string oldValue, string newValue)
   {
      int index = -1;

      foreach (var f in farmerDatabase.GetAllFarmers())
      {
         if (f.Name == FarmerName)
         {
            index = f.Id;

            foreach (var button in farmerSelectButtons)
            {
               button.button.interactable = false;
            }

            break;
         }
      }
      CmdSetRemoteOverlays(index);
   }

   public void HandleReadyStatusChanged(bool oldValue, bool newValue)
   {
      UpdateDisplay();
   }

   [ClientRpc(includeOwner = false)]
   void RpcSetRemoteOverlays(int farmer)
   {
      NetworkClient.localPlayer.GetComponent<RoomPlayer>().farmerSelectButtons[farmer].SetDisabled();
   }

   void UpdateDisplay()
   {
      if (!isOwned)
      {
         foreach (var player in Room.RoomPlayers)
         {
            if (player.isLocalPlayer)
            {
               player.UpdateDisplay();
               break;
            }
         }

         return;
      }

      for (int i = 0; i < playerNameTexts.Length; i++)
      {
         playerNameTexts[i].text = "Waiting For Player...";
         playerReadyTexts[i].text = string.Empty;
      }

      for (int i = 0; i < Room.RoomPlayers.Count; i++)
      {
         playerNameTexts[i].text = Room.RoomPlayers[i].DisplayName;
         playerReadyTexts[i].text = Room.RoomPlayers[i].IsReady ? "<color=green>Ready</color>" : "<color=red>Not Ready</color>";
      }
   }

   public void HandleReadyToStart(bool readyToStart)
   {
      if (!isLeader) return;

      startGameButton.interactable = readyToStart;
   }

   public void SetFarmer()
   {
      CmdSetFarmer(selectedFarmer.Name);
   }
   #endregion

   #region Server

   [Command]
   void CmdSetDisplayName(string displayName)
   {
      DisplayName = displayName;
   }

   [Command]
   public void CmdReadyUp()
   {
      IsReady = true;
      Room.NotifyPlayersOfReadyState();
   }

   [Command]
   public void CmdSetFarmer(string myFarmer)
   {
      FarmerName = myFarmer;
   }

   [Command]
   void CmdSetRemoteOverlays(int farmer)
   {
      RpcSetRemoteOverlays(farmer);
   }

   [Command]
   public void CmdStartGame()
   {
      if (Room.RoomPlayers[0].connectionToClient != connectionToClient) return;

      //start the game...
      Room.StartGame();
   }
   #endregion
}

