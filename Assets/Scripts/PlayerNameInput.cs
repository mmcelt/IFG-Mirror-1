using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour
{
   #region Fields & Properties

   [SerializeField] TMP_InputField nameInput;
   [SerializeField] Button continueButton;

   public static string DisplayName { get; private set; }

   public const string PlayerPrefsNameKey = "PlayerName";

   #endregion

   #region Unity Callbacks

   //void Awake() 
   //{
   //	
   //}

   void OnEnable()
   {
      //SetupInputField();
      nameInput.Select();
      nameInput.ActivateInputField();
   }

   #endregion

   void SetupInputField()
   {
      //if(!PlayerPrefs.HasKey(PlayerPrefsNameKey)) return;

      //string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);

      //nameInput.text = defaultName;

      SetPlayerName(nameInput.text);
   }

   public void OnNameInputValueChanged(string name)
   {
      SetPlayerName(name);
   }

   void SetPlayerName(string name)
   {
      continueButton.interactable = !string.IsNullOrEmpty(name);
   }

   public void SavePlayerName()
   {
      DisplayName = nameInput.text;

      //PlayerPrefs.SetString(PlayerPrefsNameKey, DisplayName);
   }
}

