using Mirror;
using UnityEngine;

public class StickerManager : SingletonMirror<StickerManager>
{
   #region Fields & Properties

   [SerializeField] GameObject[] commodityStickers;
   [SerializeField] GameObject[] equipmentStickers;
   [SerializeField] GameObject[] rangeStickers;

   GameObject stickerToPlace;

   #endregion

   #region Mirror Callbacks

 
   #endregion

   public void PlaceBoardSticker(string farmer, string type, int amount, bool isdoubled = false)
   {
      //GetStickerToPlace(type);
      //GetSpawnPosition(farmer, type);
   }

   //GameObject GetStickerToPlace(string type)
   //{
   //   switch (type)
   //   {
   //      case IFG.HAY:

   //   }
   //}
}

