using UnityEngine;

[CreateAssetMenu(fileName = "New Farmer Database", menuName = "Farmers/Database")]
public class FarmerDatabaseSO : ScriptableObject
{
   #region Fields & Properties

   public FarmerSO[] Farmers;

   #endregion

   public FarmerSO[] GetAllFarmers() => Farmers;

   public FarmerSO GetFarmerById(int id)
   {
      foreach (var farmer in Farmers)
         if(farmer.Id == id) return farmer;

      return null;
   }

}

