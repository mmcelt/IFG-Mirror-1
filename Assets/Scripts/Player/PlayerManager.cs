using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
   #region Fields & Properties

   [SerializeField] GamePlayer player;

   [SyncVar(hook = nameof(HandleCashUpdated))]
   [SerializeField] int cash;
   [SyncVar(hook = nameof(HandleNotedUpdated))]
   [SerializeField] int notes;
   [SyncVar(hook = nameof(HandleNetworthUpdated))]
   [SerializeField] int networth;
   [SyncVar(hook = nameof(HandleHayUpdated))]
   [SerializeField] int hay;
   [SyncVar(hook = nameof(HandleGrainUpdated))]
   [SerializeField] int grain;
   [SyncVar(hook = nameof(HandleFruitUpdated))]
   [SerializeField] int fruit;
   [SyncVar(hook = nameof(HandleSpudsUpdated))]
   [SerializeField] int spuds;
   [SyncVar(hook = nameof(HandleFarmCowsUpdated))]
   [SerializeField] int fCows;
   [SyncVar(hook = nameof(HandleRangeCowsUpdated))]
   [SerializeField] int rCows;
   [SyncVar(hook = nameof(HandleTractorUpdated))]
   [SerializeField] bool tractor;
   [SyncVar(hook = nameof(HandleHarvesterUpdated))]
   [SerializeField] bool harvester;
   [SyncVar(hook = nameof(HandleOxfordUpdated))]
   [SerializeField] bool oxfordOwned;
   [SyncVar(hook = nameof(HandleTargheeUpdated))]
   [SerializeField] bool targheeOwned;
   [SyncVar(hook = nameof(HandleLostRiverUpdated))]
   [SerializeField] bool lostRiverOwned;
   [SyncVar(hook = nameof(HandleLemhiUpdated))]
   [SerializeField] bool lemhiOwned;
   [SyncVar(hook = nameof(HandleOtbCountUpdated))]
   [SerializeField] int otbCount;

   public int hayCounter;
   public bool hayD, cornD, spudsD, cowsI;


   bool initialCards = true;
   List<OTBCard> myOTBs = new List<OTBCard>();

   #endregion

   #region Mirror Callbacks

   public override void OnStartAuthority()
   {
      Invoke(nameof(GetMyInitialAssets), 0.5f);
   }
   #endregion

   #region Hooks

   void HandleCashUpdated(int oldValue, int newValue)
   {
      UIManager.Instance.UpdateMyCashText(cash);
      UpdateMyNetworth(CalculateNetworth());
   }

   void HandleNotedUpdated(int oldValue, int newValue)
   {
      UIManager.Instance.UpdateMyNotesText(notes);
      UpdateMyNetworth(CalculateNetworth());
   }

   void HandleNetworthUpdated(int oldValue, int newValue)
   {
      UIManager.Instance.UpdateMyNetworthText(networth);
   }

   void HandleHayUpdated(int oldValue, int newValue)
   {
      //place sticker
      CmdPlaceSticker(player.GetFarmerName(), IFG.HAY, hay, hayD);
      //update UI
      UIManager.Instance.UpdateMyCommodityAmounts(hayCounter, hay, grain, fruit, spuds, fCows, rCows);
      UIManager.Instance.UpdateCommodityStickers(hayD, cornD, spudsD, cowsI);
      UpdateMyNetworth(CalculateNetworth());
   }

   void HandleGrainUpdated(int oldValue, int newValue)
   {
      //place sticker
      CmdPlaceSticker(player.GetFarmerName(), IFG.GRAIN, grain, cornD);
      //update UI
      UIManager.Instance.UpdateMyCommodityAmounts(hayCounter, hay, grain, fruit, spuds, fCows, rCows);
      UIManager.Instance.UpdateCommodityStickers(hayD, cornD, spudsD, cowsI);
      UpdateMyNetworth(CalculateNetworth());
   }

   void HandleFruitUpdated(int oldValue, int newValue)
   {
      //place sticker
      CmdPlaceSticker(player.GetFarmerName(), IFG.FRUIT, fruit, false);
      //update UI
      UIManager.Instance.UpdateMyCommodityAmounts(hayCounter, hay, grain, fruit, spuds, fCows, rCows);
      UIManager.Instance.UpdateCommodityStickers(hayD, cornD, spudsD, cowsI);
      UpdateMyNetworth(CalculateNetworth());
   }

   void HandleSpudsUpdated(int oldValue, int newValue)
   {
      //place sticker
      CmdPlaceSticker(player.GetFarmerName(), IFG.SPUDS, spuds, spudsD);
      //update UI
      UIManager.Instance.UpdateMyCommodityAmounts(hayCounter, hay, grain, fruit, spuds, fCows, rCows);
      UIManager.Instance.UpdateCommodityStickers(hayD, cornD, spudsD, cowsI);
      UpdateMyNetworth(CalculateNetworth());
   }

   void HandleFarmCowsUpdated(int oldValue, int newValue)
   {
      //place sticker
      CmdPlaceSticker(player.GetFarmerName(), IFG.COWS, fCows, cowsI);
      //update UI
      UIManager.Instance.UpdateMyCommodityAmounts(hayCounter, hay, grain, fruit, spuds, fCows, rCows);
      UIManager.Instance.UpdateCommodityStickers(hayD, cornD, spudsD, cowsI);
      UpdateMyNetworth(CalculateNetworth());
   }

   void HandleRangeCowsUpdated(int oldValue, int newValue)
   {
      UIManager.Instance.UpdateMyCommodityAmounts(hayCounter, hay, grain, fruit, spuds, fCows, rCows);
      UIManager.Instance.UpdateCommodityStickers(hayD, cornD, spudsD, cowsI);
      UpdateMyNetworth(CalculateNetworth());
   }

   void HandleTractorUpdated(bool oldValue, bool newValue)
   {
      //place sticker
      if (tractor)
         CmdPlaceSticker(player.GetFarmerName(), IFG.TRACTOR, 1, false);
      else
         CmdPlaceSticker(player.GetFarmerName(), IFG.TRACTOR, 0, false);
      //update UI
      UIManager.Instance.UpdateTractor(tractor);
      UpdateMyNetworth(CalculateNetworth());
   }

   void HandleHarvesterUpdated(bool oldValue,bool newValue)
   {
      //place sticker
      if (harvester)
         CmdPlaceSticker(player.GetFarmerName(), IFG.HARVESTER, 1, false);
      else
         CmdPlaceSticker(player.GetFarmerName(), IFG.HARVESTER, 0, false);
      //update UI
      UIManager.Instance.UpdateHarvester(harvester);
      UpdateMyNetworth(CalculateNetworth());
   }

   void HandleOxfordUpdated(bool oldValue,bool newValue)
   {
      //place sticker
      if (oxfordOwned)
         CmdPlaceSticker(player.GetFarmerName(), IFG.OXFORD, 1, cowsI);
      else
         CmdPlaceSticker(player.GetFarmerName(), IFG.OXFORD, 0, cowsI);
      //update UI
      UIManager.Instance.UpdateMyCommodityAmounts(hayCounter, hay, grain, fruit, spuds, fCows, rCows);
      UIManager.Instance.UpdateCommodityStickers(hayD, cornD, spudsD, cowsI);
      UpdateMyRangeCows(20);
      UpdateMyNetworth(CalculateNetworth());
   }

   void HandleTargheeUpdated(bool oldValue, bool newValue)
   {
      //place sticker
      if (targheeOwned)
         CmdPlaceSticker(player.GetFarmerName(), IFG.TARGHEE, 1, cowsI);
      else
         CmdPlaceSticker(player.GetFarmerName(), IFG.TARGHEE, 0, cowsI);
      //update UI
      UIManager.Instance.UpdateMyCommodityAmounts(hayCounter, hay, grain, fruit, spuds, fCows, rCows);
      UIManager.Instance.UpdateCommodityStickers(hayD, cornD, spudsD, cowsI);
      UpdateMyRangeCows(30);
      UpdateMyNetworth(CalculateNetworth());
   }

   void HandleLostRiverUpdated(bool oldValue, bool newValue)
   {
      //place sticker
      if (lostRiverOwned)
         CmdPlaceSticker(player.GetFarmerName(), IFG.LOST_RIVER, 1, cowsI);
      else
         CmdPlaceSticker(player.GetFarmerName(), IFG.LOST_RIVER, 0, cowsI);
      //update UI
      UIManager.Instance.UpdateMyCommodityAmounts(hayCounter, hay, grain, fruit, spuds, fCows, rCows);
      UIManager.Instance.UpdateCommodityStickers(hayD, cornD, spudsD, cowsI);
      UpdateMyRangeCows(40);
      UpdateMyNetworth(CalculateNetworth());
   }

   void HandleLemhiUpdated(bool oldValue, bool newValue)
   {
      //place sticker
      if (lemhiOwned)
         CmdPlaceSticker(player.GetFarmerName(), IFG.LEMHI, 1, cowsI);
      else
         CmdPlaceSticker(player.GetFarmerName(), IFG.LEMHI, 0, cowsI);
      //update UI
      UIManager.Instance.UpdateMyCommodityAmounts(hayCounter, hay, grain, fruit, spuds, fCows, rCows);
      UIManager.Instance.UpdateCommodityStickers(hayD, cornD, spudsD, cowsI);
      UpdateMyRangeCows(50);
      UpdateMyNetworth(CalculateNetworth());
   }

   void HandleOtbCountUpdated(int oldValue, int newValue)
   {
      UIManager.Instance.UpdateMyOtbCountText(otbCount);
   }

   #endregion

   #region Client

   void GetMyInitialAssets()
   {
      //Debug.Log("In GMHAG");
      UpdateMyCash(5000);
      UpdateMyNotes(5000);
      UpdateMyHay(10);
      UpdateMyGrain(10);
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

   int CalculateNetworth()
   {
      int bottomLine = cash - notes;
      bottomLine += hay * 2000;
      bottomLine += grain * 2000;
      bottomLine += fruit * 5000;
      bottomLine += spuds * 2000;
      bottomLine += (fCows + rCows) * 500;
      if (harvester)
         bottomLine += 10000;
      if (tractor)
         bottomLine += 10000;
      Debug.Log($"NW: {bottomLine}");
      return bottomLine;
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
      //Debug.Log($"InCMD: {amount}");
      otbCount = amount;
   }

   [Command]
   void CmdDrawOTBCard()
   {
      DeckManager.Instance.DrawOTBCard(gameObject);
   }

   [Command]
   void CmdPlaceSticker(string farmer, string type, int amount, bool doubled)
   {
      StickerManager.Instance.PlaceBoardSticker(farmer, type, amount, doubled);
   }

   #endregion
}

