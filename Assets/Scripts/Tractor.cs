using Mirror;
using UnityEngine;

public class Tractor : NetworkBehaviour
{
   #region Fields & Properties

   [SerializeField] Texture[] textures;

   public string farmerName;     //TODO: PUBLIC 4 TESTING

   public GamePlayer myPlayer;   //TODO: PUBLIC 4 TESTING

   [SerializeField] PlayerSetup pSetup;
   [SerializeField] PlayerManager pManager;

   #endregion

   #region Mirror Callbacks

   public override void OnStartAuthority()
   {
      GetMyGamePlayerStuff();
      SetFarmerName(myPlayer.GetFarmerName());
   }

   #endregion

   void SetFarmerName(string farmer)
   {
      farmerName = farmer;
   }

   public string GetFarmerName()
   {
      return farmerName;
   }

   void GetMyGamePlayerStuff()
   {
      GamePlayer[] gamePlayers = GameObject.FindObjectsOfType<GamePlayer>();

      foreach (GamePlayer player in gamePlayers)
      {
         if (player.isOwned)
         {
            myPlayer = player;
            pSetup = myPlayer.GetComponent<PlayerSetup>();
            pManager = myPlayer.GetComponent<PlayerManager>();
            break;
         }
      }
   }

   public PlayerManager GetMyPlayerManager()
   {
      return pManager;
   }

   public PlayerSetup GetMyPlayerSetup()
   {
      return pSetup;
   }
}

