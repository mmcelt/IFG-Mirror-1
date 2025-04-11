using Mirror;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSpawnSystem : NetworkBehaviour
{
   #region Fields & Properties

   [SerializeField] FarmerDatabaseSO farmerDatabase;
   [SerializeField] GameObject[] tractorPrefabs;
   [SerializeField] PlayerPositionsSO spawnpoints;

   //int nextIndex;

   #endregion

   #region Mirror Callbacks

   public override void OnStartServer()
   {
      MyNetworkManager.OnServerReadied += SpawnPlayer;
   }

   public override void OnStopServer()
   {
      MyNetworkManager.OnServerReadied -= SpawnPlayer;
   }

   public override void OnStartClient()
   {
      //InputManager.Add(ActioMapNames.Player);
      //InputManager.Controls.Player.Look.Enable();
   }
   #endregion

   [Server]
   void SpawnPlayer(NetworkConnectionToClient conn)
   {
      string farmerName = conn.identity.GetComponent<GamePlayer>().GetFarmerName();

      Vector3 spawnpoint = GetSpawnpointFromFarmer(farmerName);

      GameObject tractorInstance = Instantiate(tractorPrefabs[IFG.GetIndexFromFarmer(farmerName)], spawnpoint, Quaternion.Euler(0, 90, 0));

      conn.identity.GetComponent<GamePlayer>().SetMyTractor(tractorInstance.GetComponent<Tractor>());

      NetworkServer.Spawn(tractorInstance, conn);
   }

   public Vector3 GetSpawnpointFromFarmer(string farmer)
   {
      switch (farmer)
      {
         case IFG.RON:
            return spawnpoints.ronPos00;
         case IFG.JANIS:
            return spawnpoints.janPos00;
         case IFG.JERRY:
            return spawnpoints.jerPos00;
         case IFG.RIC:
            return spawnpoints.ricPos00;
         case IFG.MIKE:
            return spawnpoints.mikPos00;
         case IFG.BECKY:
            return spawnpoints.becPos00;
         default:
            return Vector3.zero;
      }
   }
}

