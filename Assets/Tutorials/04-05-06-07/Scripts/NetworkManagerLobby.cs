using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManagerLobby : NetworkManager
{
   #region Fields & Properties

   [SerializeField] int minPlayers = 2;
   [SerializeField] string menuScene, gameScene;
   [Header("Room")]
   [SerializeField] NetworkRoomPlayerLobby roomPlayerPrefab;
   [Header("Game")]
   [SerializeField] NetworkGamePlayerLobby gamePlayerPrefab;
   [SerializeField] GameObject playerSpawnSystem;
   [SerializeField] GameObject roundSystem;

   public static Action OnClientConnected;
   public static Action OnClientDisconnected;
   public static Action<NetworkConnectionToClient> OnServerReadied;
   public static Action OnServerStopped;

   public List<NetworkRoomPlayerLobby> RoomPlayers { get; } = new List<NetworkRoomPlayerLobby>();
   public List<NetworkGamePlayerLobby> GamePlayers { get; } = new List<NetworkGamePlayerLobby>();

   #endregion

   #region Mirror Callbacks

   public override void OnStartServer()
   {
      spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();
   }

   public override void OnStartClient()
   {
      var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

      foreach (var prefab in spawnablePrefabs)
         NetworkClient.RegisterPrefab(prefab);
   }

   public override void OnClientConnect()
   {
      base.OnClientConnect();

      OnClientConnected?.Invoke();
   }

   public override void OnClientDisconnect()
   {
      OnClientDisconnected?.Invoke();
   }

   public override void OnServerConnect(NetworkConnectionToClient conn)
   {
      if (numPlayers >= maxConnections)
      {
         conn.Disconnect();
         return;
      }

      if (SceneManager.GetActiveScene().name != menuScene)
      {
         conn.Disconnect();
         return;
      }
   }

   public override void OnServerAddPlayer(NetworkConnectionToClient conn)
   {
      if (SceneManager.GetActiveScene().name == menuScene)
      {
         bool isLeader = RoomPlayers.Count == 0;

         NetworkRoomPlayerLobby roomPlayerInstance = Instantiate(roomPlayerPrefab);

         roomPlayerInstance.IsLeader = isLeader;

         NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
      }
   }

   public override void OnServerDisconnect(NetworkConnectionToClient conn)
   {
      if (conn.identity != null)
      {
         var player = conn.identity.GetComponent<NetworkRoomPlayerLobby>();
         RoomPlayers.Remove(player);
         NotifyPlayersOfReadyState();
      }

      base.OnServerDisconnect(conn);
   }

   public override void OnStopServer()
   {
      OnServerStopped?.Invoke();

      RoomPlayers.Clear();
      GamePlayers.Clear();
   }

   public override void ServerChangeScene(string newSceneName)
   {
      //from menu to game
      if (SceneManager.GetActiveScene().name == menuScene && newSceneName == gameScene)
      {
         for (int i = RoomPlayers.Count - 1; i >= 0; i--)
         {
            var conn = RoomPlayers[i].connectionToClient;
            var gamePlayerInstance = Instantiate(gamePlayerPrefab);
            gamePlayerInstance.SetDisplayName(RoomPlayers[i].DisplayName);

            NetworkServer.Destroy(conn.identity.gameObject);

            NetworkServer.ReplacePlayerForConnection(conn, gamePlayerInstance.gameObject, ReplacePlayerOptions.KeepAuthority);
         }
      }

      base.ServerChangeScene(newSceneName);
   }

   public override void OnServerSceneChanged(string sceneName)
   {
      if (sceneName == gameScene)
      {
         GameObject playerSpawnSystemInstance = Instantiate(playerSpawnSystem);
         NetworkServer.Spawn(playerSpawnSystemInstance);

         GameObject roundSystemInstance = Instantiate(roundSystem);
         NetworkServer.Spawn(roundSystemInstance);
      }
   }

   public override void OnServerReady(NetworkConnectionToClient conn)
   {
      base.OnServerReady(conn);

      OnServerReadied?.Invoke(conn);
   }
   #endregion

   #region Server

   public void NotifyPlayersOfReadyState()
   {
      foreach (var player in RoomPlayers)
         player.HandleReadyToStart(IsReadyToStart());
   }

   bool IsReadyToStart()
   {
      if (numPlayers < minPlayers) return false;

      foreach (var player in RoomPlayers)
      {
         if (!player.IsReady) return false;
      }

      return true;
   }

   public void StartGame()
   {
      if (SceneManager.GetActiveScene().name == menuScene)
      {
         if (!IsReadyToStart()) return;

         ServerChangeScene(gameScene);
      }
   }
   #endregion
}

