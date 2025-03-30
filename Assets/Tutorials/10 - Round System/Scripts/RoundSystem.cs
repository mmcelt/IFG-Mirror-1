using Mirror;
using System.Linq;
using UnityEngine;

public class RoundSystem : NetworkBehaviour
{
   #region Fields & Properties

   [SerializeField] Animator animator;

   NetworkManagerLobby room;

   public NetworkManagerLobby Room
   {
      get
      {
         if (room != null) return room;
         return room = NetworkManager.singleton as NetworkManagerLobby;
      }
   }

   #endregion

   public void CountdownEnded()  //animation event
   {
      animator.enabled = false;
   }

   [ServerCallback]
   void OnDestroy()
   {
      CleanUpServer();
   }

   #region Mirror Callbacks

   public override void OnStartServer()
   {
      NetworkManagerLobby.OnServerStopped += CleanUpServer;
      NetworkManagerLobby.OnServerReadied += CheckToStartRound;
   }
   #endregion

   #region Server

   [Server]
   void CleanUpServer()
   {
      NetworkManagerLobby.OnServerStopped -= CleanUpServer;
      NetworkManagerLobby.OnServerReadied -= CheckToStartRound;
   }

   [ServerCallback]
   public void StartRound()   //animation event
   {
      RpcStartRound();
   }

   [Server]
   void CheckToStartRound(NetworkConnection conn)
   {
      if (Room.GamePlayers.Count(x => x.connectionToClient.isReady) != Room.GamePlayers.Count) return;

      animator.enabled = true;

      RpcStartCountDown();
   }
   #endregion

   #region Client

   [ClientRpc]
   void RpcStartCountDown()
   {
      animator.enabled = true;
   }

   [ClientRpc]
   void RpcStartRound()
   {
      //InputManager.Remove(ActioMapNames.Player);
   }
   #endregion

}

