using UnityEngine;

public class MainMenu : MonoBehaviour
{
   #region Fields & Properties

   [SerializeField] NetworkManagerLobby networkManager;
   [SerializeField] GameObject landingPagePanel;

   #endregion

   #region Unity Callbacks

   //void Awake() 
   //{
   //	
   //}

   //void Start() 
   //{
   //	
   //}

   //void Update()
   //{
   //   
   //}

   #endregion

   public void HostLobby()
   {
      networkManager.StartHost();
      landingPagePanel.SetActive(false);
   }
}

