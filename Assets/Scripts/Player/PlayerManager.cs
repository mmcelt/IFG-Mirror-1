using Mirror;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
   #region Fields & Properties

   [SerializeField] GamePlayer player;

   [SyncVar]
   int cash;
   [SyncVar]
   int notes;
   [SyncVar]
   int networth;
   [SyncVar]
   int hay;
   [SyncVar]
   int grain;
   [SyncVar]
   int fruit;
   [SyncVar]
   int spuds;
   [SyncVar]
   int fCows;
   [SyncVar]
   int rCows;
   [SyncVar]
   bool tractor;
   [SyncVar]
   bool harvester;

   #endregion

   #region Mirror Callbacks

   public override void OnStartAuthority()
   {
      
   }
   #endregion

   #region Client

   public void UpdateMyCash(int amount)
   {
      CmdUpdateMyCash(amount);
   }

   public int GetMyCash()
   {
      return cash;
   }

   public void UpdateMyNotes(int amount)
   {
      CmdUpdateMyNotes(amount);
   }

   public int GetMyNotes()
   {
      return notes;
   }

   public void UpdateMyNetworth(int amount)
   {
      CmdUpdateMyNetworth(amount);
   }

   public int GetMyNetworth()
   {
      return networth;
   }

   public void UpdateMyHay(int amount)
   {
      CmdUpdateMyHay(amount);
   }

   public int GetMyHay()
   {
      return hay;
   }

   public void UpdateMyGrain(int amount)
   {
      CmdUpdateMyGrain(amount);
   }

   public int GetMyGrain()
   {
      return grain;
   }

   public void UpdateMyFruit(int amount)
   {
      CmdUpdateMyFruit(amount);
   }

   public int GetMyFruit()
   {
      return fruit;
   }

   public void UpdateMySpuds(int amount)
   {
      CmdUpdateMySpuds(amount);
   }

   public int GetMySpuds()
   {
      return spuds;
   }

   public void UpdateMyFarmCows(int amount)
   {
      CmdUpdateMyFarmCows(amount);
   }

   public int GetMyFarmCows()
   {
      return fCows;
   }
 
   public void UpdateMyRangeCows(int amount)
   {
      CmdUpdateMyRangeCows(amount);
   }

   public int GetMyRangeCows()
   {
      return rCows;
   }

   public void UpdateMyTractor(bool owned)
   {
      CmdUpdateMyTractor(owned); 
   }

   public bool GetMyTractor()
   {
      return tractor;
   }

   public void UpdateMyHarvester(bool owned)
   {
      CmdUpdateMyHarvester(owned);
   }

   public bool GetMyHarvester()
   {
      return harvester;
   }
   #endregion

   #region Server

   [Command]
   void CmdUpdateMyCash(int amount)
   {
      cash += amount;
   }

   [Command]
   void CmdUpdateMyNotes(int amount)
   {
      notes += amount;
   }

   [Command]
   void CmdUpdateMyNetworth(int amount)
   {
      networth = amount;
   }

   [Command]
   void CmdUpdateMyHay(int amount)
   {
      hay += amount;
   }

   [Command]
   void CmdUpdateMyGrain(int amount)
   {
      grain += amount;
   }

   [Command]
   void CmdUpdateMyFruit(int amount)
   {
      fruit += amount;
   }

   [Command]
   void CmdUpdateMySpuds(int amount)
   {
      spuds += amount;
   }

   [Command]
   void CmdUpdateMyFarmCows(int amount)
   {
      fCows += amount;
   }

   [Command]
   void CmdUpdateMyRangeCows(int amount)
   {
      rCows += amount;
   }

   [Command]
   void CmdUpdateMyTractor(bool owned)
   {
      tractor = owned;
   }

   [Command]
   void CmdUpdateMyHarvester(bool owned)
   {
      harvester = owned;
   }
   #endregion
}

