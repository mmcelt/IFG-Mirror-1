using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{
   #region Fields & Properties

   [SerializeField] MyNetworkManager networkManager;
   [Header("UI")]
   [SerializeField] GameObject landingPagePanel;
   [SerializeField] TMP_InputField ipAddressInput;
   [SerializeField] Button joinButton;

   #endregion

   #region Unity Callbacks

   void OnEnable()
   {
      MyNetworkManager.OnClientConnected += HandleClientConnected;
      MyNetworkManager.OnClientConnected += HandleClientDisconnected;
   }

   void OnDisable()
   {
      MyNetworkManager.OnClientConnected -= HandleClientConnected;
      MyNetworkManager.OnClientConnected -= HandleClientDisconnected;
   }
   #endregion

   public void JoinLobby()
   {
      string ipAddress = ipAddressInput.text;

      networkManager.networkAddress = ipAddress;
      networkManager.StartClient();

      //joinButton.interactable = false;
   }

   void HandleClientConnected()
   {
      joinButton.interactable = true;
      gameObject.SetActive(false);
      landingPagePanel.SetActive(false);
   }

   void HandleClientDisconnected()
   {
      joinButton.interactable = true;
   }
}

