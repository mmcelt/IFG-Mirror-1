using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

public class IFG
{
   #region Player Names

   public const string RON = "Rigby Ron";
   public const string JANIS = "Jensen Janis";
   public const string JERRY = "Jolley Jerry";
   public const string RIC = "Roberts Ric";
   public const string BECKY = "Bassett Becky";
   public const string MIKE = "Menan Mike";

   #endregion

   //Colors
   public static Color Purple = new Color(0.5f, 0f, 0.5f);

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
         case RON:
            return 0;
         case JANIS:
            return 1;
         case JERRY:
            return 2;
         case RIC:
            return 3;
         case BECKY:
            return 4;
         case MIKE:
            return 5;
         default:
            Debug.LogError($"FUBAR FARMER! {farmer}");
            return 0;
      }
   }

   public static Color GetColorFromFarmer(string farmer)
   {
      switch (farmer)
      {
         case RON:
            return Color.blue;
            case JANIS:
            return Color.red;
            case JERRY:
            return Purple;
            case RIC:
            return Color.black;
            case BECKY:
            return Color.white;
            case MIKE:
            return Color.yellow;
         default:
            Debug.LogWarning($"FURAR Farmer: {farmer}");
            return Color.cyan;
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

