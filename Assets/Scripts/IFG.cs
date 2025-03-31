using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

public class IFG
{
   #region Player Names

   public const string Ron = "Rigby Ron";
   public const string Janis = "Jensen Janis";
   public const string Jerry = "Jolley Jerry";
   public const string Ric = "Roberts Ric";
   public const string Becky = "Bassett Becky";
   public const string Mike = "Menan Mike";

   #endregion

   //Crops / Equipment
   public const string Hay = "Hay";
   public const string Grain = "Grain";
   public const string Fruit = "Fruit";
   public const string Spuds = "Spuds";
   public const string Cows = "Cow";
   public const string Tractor = "Tractor";
   public const string Harvester = "Harvester";

   //Ranges
   public const string Oxford = "Oxford";
   public const string Targhee = "Targhee";
   public const string LostRiver = "Lost River";
   public const string Lemhi = "Lemhi";

   //Harvests
   public const string HayH = "Hay";
   public const string CherryH = "Cherries";
   public const string WheatH = "Wheat";
   public const string LivestockH = "Livestock";
   public const string SpudH = "Spuds";
   public const string AppleH = "Apples";
   public const string CornH = "Corn";

   public static int GetIndexFromFarmer(string farmer)
   {
      switch (farmer)
      {
         case Ron:
            return 0;
         case Janis:
            return 1;
         case Jerry:
            return 2;
         case Ric:
            return 3;
         case Becky:
            return 4;
         case Mike:
            return 5;
         default:
            Debug.LogError($"FUBAR FARMER! {farmer}");
            return 0;
      }
   }

   #region Enums

   public enum GameType
   {
      NETWORTH,
      TIMED
   }
   #endregion

}

