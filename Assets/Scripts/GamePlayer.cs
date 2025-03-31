using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayer : NetworkBehaviour
{
   #region Fields & Properties

   [SyncVar]
   [SerializeField] string displayName = "Not Set...";
   [SyncVar]
   [SerializeField] string farmerName = "Not Set";

   MyNetworkManager room;

   public MyNetworkManager Room
   {
      get
      {
         if (room != null) return room;
         return room = NetworkManager.singleton as MyNetworkManager;
      }
   }

   #endregion

   #region Mirror Callbacks

   public override void OnStartClient()
   {
      DontDestroyOnLoad(gameObject);
      Room.GamePlayers.Add(this);
   }

   public override void OnStopClient()
   {
      Room.GamePlayers.Remove(this);
   }
   #endregion

   #region Client


   #endregion

   #region Server

   [Server]
   public void SetDisplayName(string newName)
   {
      displayName = newName;
   }

   [Server]
   public void SetFarmerName(string newName)
   {
      farmerName = newName;
   }

   #endregion

   #region Client

   public string GetFarmerName()
   {
      return farmerName;
   }
   #endregion
}

