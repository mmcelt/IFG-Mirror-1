using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
   #region Fields & Properties

   [SerializeField] GamePlayer player;

   [SyncVar]
   [SerializeField] int cash;
   [SyncVar]
   [SerializeField] int notes;
   [SyncVar]
   [SerializeField] int networth;
   [SyncVar]
   [SerializeField] int hay;
   [SyncVar]
   [SerializeField] int grain;
   [SyncVar]
   [SerializeField] int fruit;
   [SyncVar]
   [SerializeField] int spuds;
   [SyncVar]
   [SerializeField] int fCows;
   [SyncVar]
   [SerializeField] int rCows;
   [SyncVar]
   [SerializeField] bool tractor;
   [SyncVar]
   [SerializeField] bool harvester;
   [SyncVar(hook = nameof(HandleOtbCountChanged))]
   [SerializeField] int otbCount;

   bool initialCards = true;

   List<OTBCard> myOTBs = new List<OTBCard>();

   #endregion

   #region Mirror Callbacks


   #endregion

   #region Client

   void HandleOtbCountChanged(int oldValue, int newValue)
   {
      UIManager.Instance.UpdateMyOtbCountText(otbCount);
   }

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

   public void DrawOTBCard()
   {
      CmdDrawOTBCard();
   }

   public void ReceiveOTBCard(OTBCard drawnCard)
   {
      drawnCard.bottomCard = false;
      myOTBs.Add(drawnCard);
      //Debug.Log($"Got card: {drawnCard.cardNumber}::{myOTBs.Count}");

      CmdUpateMyOtbCount(myOTBs.Count);

      if (initialCards && myOTBs.Count == 2)
      {
         initialCards = false;
         return;
      }
      else if (!initialCards)
      {
         //StartCoroutine(ShowOtbCardRoutine(drawnCard));
      }
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

   [Command]
   void CmdUpateMyOtbCount(int amount)
   {
      Debug.Log($"InCMD: {amount}");
      otbCount = amount;
   }

   [Command]
   void CmdDrawOTBCard()
   {
      DeckManager.Instance.DrawOTBCard(gameObject);
   }
   #endregion
}

