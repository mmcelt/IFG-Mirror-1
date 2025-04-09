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
   public static Color GreyedOut = new Color(0.5943396f, 0.5943396f, 0.5943396f);

   //Crops / Equipment
   public const string HAY = "Hay";
   public const string GRAIN = "Grain";
   public const string FRUIT = "Fruit";
   public const string SPUDS = "Spuds";
   public const string COWS = "Cow";
   public const string TRACTOR = "Tractor";
   public const string HARVESTER = "Harvester";

   //Ranges
   public const string OXFORD = "Oxford";
   public const string TARGHEE = "Targhee";
   public const string LOST_RIVER = "Lost River";
   public const string LEMHI = "Lemhi";

   //Harvests
   public const string HayH = "Hay";
   public const string CherryH = "Cherries";
   public const string WheatH = "Wheat";
   public const string LivestockH = "Livestock";
   public const string SpudH = "Spuds";
   public const string AppleH = "Apples";
   public const string CornH = "Corn";

   //Methods
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

   public static int LoadedDie(int die)
   {
      if (Input.GetKey(KeyCode.Alpha1))
      {
         return 1;
      }
      if (Input.GetKey(KeyCode.Alpha2))
      {
         return 2;
      }
      if (Input.GetKey(KeyCode.Alpha3))
      {
         return 3;
      }
      if (Input.GetKey(KeyCode.Alpha4))
      {
         return 4;
      }
      if (Input.GetKey(KeyCode.Alpha5))
      {
         return 5;
      }
      if (Input.GetKey(KeyCode.Alpha6))
      {
         return 6;
      }
      if (Input.GetKey(KeyCode.Alpha7))
      {
         return 7;
      }
      if (Input.GetKey(KeyCode.Alpha8))
      {
         return 8;
      }
      if (Input.GetKey(KeyCode.Alpha9))
      {
         return 9;
      }

      return die;
   }

   #region Enums

   public enum GameType
   {
      NETWORTH,
      TIMED
   }
   #endregion

}

