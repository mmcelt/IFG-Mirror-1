using UnityEngine;

[CreateAssetMenu(fileName = "New Farmer", menuName = "Farmers/Farmer")]
public class FarmerSO : ScriptableObject
{
   #region Fields & Properties

   public int Id;
   public Sprite Icon;
   public string Name;
   public Color Color;
   public GameObject introPrefab;
   public GameObject gamePlayerPrefab;

   #endregion

}

