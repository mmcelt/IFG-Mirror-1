using Mirror;
using UnityEngine;

public class Tractor : NetworkBehaviour
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

   public void SetFarmerName(string farmer)
   {
      farmerName = farmer;
      //ChangeColor();
   }

   public string GetFarmerName()
   {
      return farmerName;
   }

   void ChangeColor()
   {
      Renderer[] renderers = GetComponentsInChildren<Renderer>();

      foreach (Renderer r in renderers)
      {
         if (r.name == "tractor")
         {
            Texture tex = textures[IFG.GetIndexFromFarmer(farmerName)];
            Debug.Log($"In CC");
            r.material.SetTexture("_BaseMap", tex);
         }
      }
   }

}

