using UnityEngine;

[CreateAssetMenu(fileName = "New Farmer Database", menuName = "Farmers/Database")]
public class FarmerDatabaseSO : ScriptableObject
{
   #region Fields & Properties

   public FarmerSO[] Farmers;

   #endregion


}

