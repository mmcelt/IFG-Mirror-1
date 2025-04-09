using UnityEngine;

public class HarvestCharts : MonoBehaviour
{
   #region Fields & Properties

   [Header("Harvest Chart Modifiers")]
   [SerializeField] float hayFudgeFactor = 1.10F;
   [SerializeField] float fruitFudgeFactor = 1.20F;
   [SerializeField] float spudFudgeFactor = 2.0F;

   #endregion

   public int GetHayCheck(int die, int hay)
   {
      int baseAmt = 0;

      switch (die)
      {
         case 1:
            baseAmt = (int)(400 * hayFudgeFactor);
            break;

         case 2:
            baseAmt = (int)(600 * hayFudgeFactor);
            break;

         case 3:
            baseAmt = (int)(1000 * hayFudgeFactor);
            break;

         case 4:
            baseAmt = (int)(1500 * hayFudgeFactor);
            break;

         case 5:
            baseAmt = (int)(2200 * hayFudgeFactor);
            break;

         case 6:
            baseAmt = (int)(3000 * hayFudgeFactor);
            break;
      }

      int calcHay = hay / 10;

      return baseAmt * calcHay;
   }

   public int GetFruitCheck(int die, int fruit)
   {
      int baseAmt = 0;

      switch (die)
      {
         case 1:
            baseAmt = (int)(2000 * fruitFudgeFactor);
            break;

         case 2:
            baseAmt = (int)(3500 * fruitFudgeFactor);
            break;

         case 3:
            baseAmt = (int)(6000 * fruitFudgeFactor);
            break;

         case 4:
            baseAmt = (int)(9000 * fruitFudgeFactor);
            break;

         case 5:
            baseAmt = (int)(13000 * fruitFudgeFactor);
            break;

         case 6:
            baseAmt = (int)(17500 * fruitFudgeFactor);
            break;
      }

      int calcFruit = fruit / 5;

      return baseAmt * calcFruit;
   }

   public int GetGrainCheck(int die, int grain)
   {
      int baseAmt = 0;
      int calcGrain = grain / 10;

      if (die == 2 || die == 3 || die == 6)
      {
         switch (die)
         {
            case 2:
               baseAmt = 1500;
               break;

            case 3:
               baseAmt = 2500;
               break;

            case 6:
               baseAmt = 7000;
               break;
         }
         return baseAmt * calcGrain;
      }
      else if (die == 1)
      {
         switch (grain)
         {
            case 10:
               baseAmt = 800;
               break;

            case 20:
               baseAmt = 1500;
               break;

            case 30:
               baseAmt = 2300;
               break;

            case 40:
               baseAmt = 3000;
               break;

            case 50:
               baseAmt = 3800;
               break;

            case 60:
               baseAmt = 4500;
               break;

            case 70:
               baseAmt = 5300;
               break;

            case 80:
               baseAmt = 6000;
               break;

            case 90:
               baseAmt = 6800;
               break;

            case 100:
               baseAmt = 7600;
               break;
         }
         return baseAmt;
      }
      else if (die == 4)
      {
         switch (grain)
         {
            case 10:
               baseAmt = 3800;
               break;

            case 20:
               baseAmt = 7500;
               break;

            case 30:
               baseAmt = 11300;
               break;

            case 40:
               baseAmt = 15000;
               break;

            case 50:
               baseAmt = 18800;
               break;

            case 60:
               baseAmt = 22500;
               break;

            case 70:
               baseAmt = 26300;
               break;

            case 80:
               baseAmt = 30000;
               break;

            case 90:
               baseAmt = 33800;
               break;

            case 100:
               baseAmt = 37600;
               break;
         }
         return baseAmt;
      }
      else if (die == 5)
      {
         switch (grain)
         {
            case 10:
               baseAmt = 5300;
               break;

            case 20:
               baseAmt = 10500;
               break;

            case 30:
               baseAmt = 15800;
               break;

            case 40:
               baseAmt = 21000;
               break;

            case 50:
               baseAmt = 26300;
               break;

            case 60:
               baseAmt = 31500;
               break;

            case 70:
               baseAmt = 36800;
               break;

            case 80:
               baseAmt = 42000;
               break;

            case 90:
               baseAmt = 47300;
               break;

            case 100:
               baseAmt = 52600;
               break;
         }
      }
      return baseAmt;
   }

   public int GetCowCheck(int die, int cows)
   {
      int baseAmt = 0;

      switch (die)
      {
         case 1:
            baseAmt = 1400;
            break;

         case 2:
            baseAmt = 2000;
            break;

         case 3:
            baseAmt = 2800;
            break;

         case 4:
            baseAmt = 3800;
            break;

         case 5:
            baseAmt = 5000;
            break;

         case 6:
            baseAmt = 7500;
            break;
      }

      int calcCows = cows / 10;

      return baseAmt * calcCows;
   }

   public int GetSpudCheck(int die, int spuds)
   {
      int baseAmt = 0;

      switch (die)
      {
         case 1:
            baseAmt = (int)(800 * spudFudgeFactor);
            break;

         case 2:
            baseAmt = (int)(1500 * spudFudgeFactor);
            break;

         case 3:
            baseAmt = (int)(2500 * spudFudgeFactor);
            break;

         case 4:
            baseAmt = (int)(3800 * spudFudgeFactor);
            break;

         case 5:
            baseAmt = (int)(5300 * spudFudgeFactor);
            break;

         case 6:
            baseAmt = (int)(7000 * spudFudgeFactor);
            break;
      }

      int calcSpuds = spuds / 10;

      return baseAmt * calcSpuds;
   }
}

