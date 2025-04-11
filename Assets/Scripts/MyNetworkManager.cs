using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyNetworkManager : NetworkManager
{
   #region Fields & Properties

   [SerializeField] int minPlayers = 1;
   [SerializeField] string menuScene, gameScene;
   [Header("Room")]
   [SerializeField] RoomPlayer roomPlayerPrefab;
   [Header("Game")]
   [SerializeField] GamePlayer gamePlayerPrefab;
   [SerializeField] GameObject playerSpawnSystemPrefab;
   [SerializeField] GameObject gameManagerPrefab;
   [SerializeField] GameObject deckManagerPrefab;

   public static Action OnClientConnected;
   public static Action OnClientDisconnected;
   public static Action<NetworkConnectionToClient> OnServerReadied;
   public static Action OnServerStopped;

   public List<RoomPlayer> RoomPlayers { get; } = new List<RoomPlayer>();
   public List<GamePlayer> GamePlayers { get; } = new List<GamePlayer>();

   //game info
   public IFG.GameType GameType;
   public int NetworthAmount;
   public float TimedLength;
   public bool BorgDie;

   #endregion

   public void SetMinPlayers(int amount)
   {
      minPlayers = amount;
   }

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

         RoomPlayer roomPlayerInstance = Instantiate(roomPlayerPrefab);

         roomPlayerInstance.IsLeader = isLeader;
         roomPlayerInstance.SetNOP(minPlayers);

         NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
      }
   }

   public override void OnServerDisconnect(NetworkConnectionToClient conn)
   {
      if (conn.identity != null)
      {
         var player = conn.identity.GetComponent<RoomPlayer>();
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
            gamePlayerInstance.SetFarmerName(RoomPlayers[i].FarmerName);
            gamePlayerInstance.SetNOP(minPlayers);
            gamePlayerInstance.SetGameType(GameType);
            gamePlayerInstance.SetNetworthAmount(NetworthAmount);
            gamePlayerInstance.SetTimedLength(TimedLength);
            gamePlayerInstance.SetBorgDie(BorgDie);

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
         GameObject playerSpawnSystemInstance = Instantiate(playerSpawnSystemPrefab);
         NetworkServer.Spawn(playerSpawnSystemInstance);

         GameObject deckManagerInstnace=Instantiate(deckManagerPrefab);
         NetworkServer.Spawn(deckManagerInstnace);

         GameObject gameManagerInstance=Instantiate(gameManagerPrefab);
         NetworkServer.Spawn(gameManagerInstance);
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
      {
         player.HandleReadyToStart(IsReadyToStart());

      }
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

