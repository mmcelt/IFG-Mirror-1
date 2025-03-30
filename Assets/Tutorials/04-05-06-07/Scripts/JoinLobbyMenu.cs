using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{
   #region Fields & Properties

   [SerializeField] NetworkManagerLobby networkManager;
   [Header("UI")]
   [SerializeField] GameObject landingPagePanel;
   [SerializeField] TMP_InputField ipAddressInput;
   [SerializeField] Button joinButton;

   #endregion

   #region Unity Callbacks

   void OnEnable()
   {
      NetworkManagerLobby.OnClientConnected += HandleClientConnected;
      NetworkManagerLobby.OnClientConnected += HandleClientDisconnected;
   }

   void OnDisable()
   {
      NetworkManagerLobby.OnClientConnected -= HandleClientConnected;
      NetworkManagerLobby.OnClientConnected -= HandleClientDisconnected;
   }
   #endregion

   public void JoinLobby()
   {
      string ipAddress = ipAddressInput.text;

      networkManager.networkAddress = ipAddress;
      networkManager.StartClient();

      joinButton.interactable = false;
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

