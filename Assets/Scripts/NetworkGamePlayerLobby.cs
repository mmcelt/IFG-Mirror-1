using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkGamePlayerLobby : NetworkBehaviour
{
   #region Fields & Properties

   [SyncVar]
   string displayName = "Loading...";
   public GameObject MyPlayerPiece;

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

   #endregion
}

