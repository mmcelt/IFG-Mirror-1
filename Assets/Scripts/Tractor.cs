using UnityEngine;

public class Tractor : MonoBehaviour
{
   #region Fields & Properties

   [SerializeField] Texture[] textures;

   [SerializeField] string farmerName;

   #endregion

   #region Unity Callbacks

   //void Awake() 
   //{
   //	
   //}

   //void Start()
   //{

   //}

   //void Update() 
   //{
   //	
   //}
   #endregion

   public void SetColorFromFarmer(string farmer)
   {
      farmerName = farmer;
      ChangeColor();
   }

   void ChangeColor()
   {
      Renderer[] renderers = GetComponentsInChildren<Renderer>();

      foreach (Renderer r in renderers)
      {
         if (r.name == "tractor")
         {
            Debug.Log($"In CC");
            r.material.SetTexture("_BaseMap", textures[IFG.GetIndexFromFarmer(farmerName)]);
         }
      }
   }
}

