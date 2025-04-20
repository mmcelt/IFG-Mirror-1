using Mirror;
using TMPro;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class StickerManager : SingletonMirror<StickerManager>
{
   #region Fields & Properties

   [SerializeField] StickerLocationsSO stickerLocations;
   [SerializeField] GameObject[] commodityStickers;
   [SerializeField] GameObject[] equipmentStickers;
   [SerializeField] GameObject[] rangeStickers;

   GameObject stickerToPlace;
   Vector3 spawnPosition;
   bool doubled;

   #endregion

   #region Mirror Callbacks


   #endregion

   [Server]
   public void PlaceBoardSticker(string farmer, string type, int amount, bool isdoubled = false)
   {
      spawnPosition = GetSpawnPosition(farmer, type);
      doubled = isdoubled;

      if (amount > 0)
      {
         stickerToPlace = GetStickerToPlace(type);
      }
      else
      {
         RemoveSticker();
         return;
      }

      //equpment
      if (type == IFG.TRACTOR || type == IFG.HARVESTER)
      {
         PlaceEquipmentSticker(amount);
         return;
      }
      //ranges
      if (type == IFG.OXFORD || type == IFG.TARGHEE || type == IFG.LOST_RIVER || type == IFG.LEMHI)
      {
         PlaceRangeSticker(amount, farmer);
         return;
      }
      //fCows
      if (type == IFG.COWS)
      {
         amount /= 10;
      }
      //hay,grain,fruit,spuds,fCows
      SpawnStandardSticker(type, amount, spawnPosition, doubled);
   }

   GameObject GetStickerToPlace(string type)
   {
      switch (type)
      {
         case IFG.HAY:
            return commodityStickers[0];
         case IFG.GRAIN:
            return commodityStickers[1];
         case IFG.FRUIT:
            return commodityStickers[2];
         case IFG.SPUDS:
            return commodityStickers[3];
         case IFG.COWS:
            return commodityStickers[4];
         case IFG.TRACTOR:
            return equipmentStickers[0];
         case IFG.HARVESTER:
            return equipmentStickers[1];
         case IFG.OXFORD:
            return rangeStickers[0];
         case IFG.TARGHEE:
            return rangeStickers[1];
         case IFG.LOST_RIVER:
            return rangeStickers[2];
         case IFG.LEMHI:
            return rangeStickers[3];
         default:
            return null;
      }
   }

   Vector3 GetSpawnPosition(string farmer, string type)
   {
      //Debug.Log($"In GPS: {farmer}::{type}");
      switch (type)
      {
         case IFG.HAY:
            if (farmer == IFG.RON)
               return stickerLocations.ronHay;
            if (farmer == IFG.JANIS)
               return stickerLocations.janHay;
            if (farmer == IFG.JERRY)
               return stickerLocations.jerHay;
            if (farmer == IFG.RIC)
               return stickerLocations.ricHay;
            if (farmer == IFG.BECKY)
               return stickerLocations.becHay;
            if (farmer == IFG.MIKE)
               return stickerLocations.mikHay;
            break;
         case IFG.GRAIN:
            if (farmer == IFG.RON)
               return stickerLocations.ronGrain;
            if (farmer == IFG.JANIS)
               return stickerLocations.janGrain;
            if (farmer == IFG.JERRY)
               return stickerLocations.jerGrain;
            if (farmer == IFG.RIC)
               return stickerLocations.ricGrain;
            if (farmer == IFG.BECKY)
               return stickerLocations.becGrain;
            if (farmer == IFG.MIKE)
               return stickerLocations.mikGrain;
            break;
         case IFG.FRUIT:
            if (farmer == IFG.RON)
               return stickerLocations.ronFruit;
            if (farmer == IFG.JANIS)
               return stickerLocations.janFruit;
            if (farmer == IFG.JERRY)
               return stickerLocations.jerFruit;
            if (farmer == IFG.RIC)
               return stickerLocations.ricFruit;
            if (farmer == IFG.BECKY)
               return stickerLocations.becFruit;
            if (farmer == IFG.MIKE)
               return stickerLocations.mikFruit;
            break;
         case IFG.SPUDS:
            if (farmer == IFG.RON)
               return stickerLocations.ronSpuds;
            if (farmer == IFG.JANIS)
               return stickerLocations.janSpuds;
            if (farmer == IFG.JERRY)
               return stickerLocations.jerSpuds;
            if (farmer == IFG.RIC)
               return stickerLocations.ricSpuds;
            if (farmer == IFG.BECKY)
               return stickerLocations.becSpuds;
            if (farmer == IFG.MIKE)
               return stickerLocations.mikSpuds;
            break;
         case IFG.COWS:
            if (farmer == IFG.RON)
               return stickerLocations.ronFarmCows;
            if (farmer == IFG.JANIS)
               return stickerLocations.janFarmCows;
            if (farmer == IFG.JERRY)
               return stickerLocations.jerFarmCows;
            if (farmer == IFG.RIC)
               return stickerLocations.ricFarmCows;
            if (farmer == IFG.BECKY)
               return stickerLocations.becFarmCows;
            if (farmer == IFG.MIKE)
               return stickerLocations.mikFarmCows;
            break;
         case IFG.TRACTOR:
            if (farmer == IFG.RON)
               return stickerLocations.ronTractor;
            if (farmer == IFG.JANIS)
               return stickerLocations.janTractor;
            if (farmer == IFG.JERRY)
               return stickerLocations.jerTractor;
            if (farmer == IFG.RIC)
               return stickerLocations.ricTractor;
            if (farmer == IFG.BECKY)
               return stickerLocations.becTractor;
            if (farmer == IFG.MIKE)
               return stickerLocations.mikTractor;
            break;
         case IFG.HARVESTER:
            if (farmer == IFG.RON)
               return stickerLocations.ronHarvester;
            if (farmer == IFG.JANIS)
               return stickerLocations.janHarvester;
            if (farmer == IFG.JERRY)
               return stickerLocations.jerHarvester;
            if (farmer == IFG.RIC)
               return stickerLocations.ricHarvester;
            if (farmer == IFG.BECKY)
               return stickerLocations.becHarvester;
            if (farmer == IFG.MIKE)
               return stickerLocations.mikHarvester;
            break;
         case IFG.OXFORD:
            return stickerLocations.oxfordRange;
         case IFG.TARGHEE:
            return stickerLocations.targheeRange;
         case IFG.LOST_RIVER:
            return stickerLocations.lostRiverRange;
         case IFG.LEMHI:
            return stickerLocations.lemhiRange;
      }
      return Vector3.zero;
   }

   void PlaceEquipmentSticker(int amount)
   {
      RemoveSticker();

      SpawnEquipmentSticker();
   }

   void PlaceRangeSticker(int amount, string farmer)
   {
      RemoveSticker();

      SpawnRangeSticker(farmer);
   }

   void RemoveSticker()
   {
      //Debug.Log("In RS");

      GameObject[] allStickers = GameObject.FindGameObjectsWithTag("Sticker");

      for (int i = allStickers.Length - 1; i >= 0; i--)
      {
         if (allStickers[i].transform.position == spawnPosition)
         {
            NetworkServer.Destroy(allStickers[i]);
            break;
         }
      }
   }

   void SpawnEquipmentSticker()
   {
      GameObject instance = Instantiate(stickerToPlace);
      instance.transform.position = spawnPosition;
      instance.transform.rotation = Quaternion.identity;
      NetworkServer.Spawn(instance);
   }

   void SpawnRangeSticker(string farmer)
   {
      GameObject instance = Instantiate(stickerToPlace, spawnPosition, Quaternion.identity);
      instance.GetComponent<Range>().Initialize(farmer, doubled);
      NetworkServer.Spawn(instance);
   }

   void SpawnStandardSticker(string type, int amount, Vector3 spawnPosition, bool doubled)
   {
      //Debug.Log($"In CmdSSS: {type}:{amount}");
      GameObject instance = Instantiate(stickerToPlace);
      instance.transform.position = spawnPosition;
      instance.transform.rotation = Quaternion.identity;
      instance.GetComponentInChildren<TMP_Text>().text = amount.ToString();

      if (doubled)
         instance.transform.localEulerAngles = new Vector3(0, 90, 0);

      NetworkServer.Spawn(instance);
   }
}

