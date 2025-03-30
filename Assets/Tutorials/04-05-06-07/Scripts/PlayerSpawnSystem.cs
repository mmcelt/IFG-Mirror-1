using Mirror;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSpawnSystem : NetworkBehaviour
{
   #region Fields & Properties

   [SerializeField] GameObject playerPrefab;

   static List<Transform> spawnPoints = new List<Transform>();

   int nextIndex;

   #endregion

   #region Mirror Callbacks

   public override void OnStartServer()
   {
      NetworkManagerLobby.OnServerReadied += SpawnPlayer;
   }

   public override void OnStopServer()
   {
      NetworkManagerLobby.OnServerReadied -= SpawnPlayer;
   }

   public override void OnStartClient()
   {
      //InputManager.Add(ActioMapNames.Player);
      //InputManager.Controls.Player.Look.Enable();
   }
   #endregion

   public static void AddSpawnPoint(Transform transform)
   {
      spawnPoints.Add(transform);

      spawnPoints = spawnPoints.OrderBy(x => x.GetSiblingIndex()).ToList();
   }

   public static void RemoveSpawnPoint(Transform transform) => spawnPoints.Remove(transform);

   [Server]
   void SpawnPlayer(NetworkConnectionToClient conn)
   {
      Transform spawnpoint = spawnPoints.ElementAtOrDefault(nextIndex);

      if (!spawnpoint)
      {
         Debug.LogError($"Missing Spawnpoint for player: {nextIndex}");
         return;
      }

      GameObject playerInstance = Instantiate(playerPrefab, spawnPoints[nextIndex].position, spawnPoints[nextIndex].rotation);

      //ADDED THIS FOR MY USEAGE
      conn.identity.GetComponent<NetworkGamePlayerLobby>().MyPlayerPiece = playerInstance;

      NetworkServer.Spawn(playerInstance, conn);

      nextIndex++;
   }
}

