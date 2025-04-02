using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
   #region Fields & Properties

   [SerializeField] MyNetworkManager networkManager;
   [SerializeField] TMP_InputField nameInput;
   [SerializeField] GameObject landingPagePanel, createGamePanel;

   #endregion

   #region Unity Callbacks

   //void Awake()
   //{
   //   nameInput.Select();
   //}

   void Start()
   {
      nameInput.ActivateInputField();
      nameInput.Select();
   }

   //void Update()
   //{
   //   
   //}

   #endregion

   public void HostLobby()
   {
      networkManager.StartHost();
      landingPagePanel.SetActive(false);
      //createGamePanel.SetActive(true);
   }
}

