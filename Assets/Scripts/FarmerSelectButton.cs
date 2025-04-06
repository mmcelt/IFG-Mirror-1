using UnityEngine;
using UnityEngine.UI;

public class FarmerSelectButton : MonoBehaviour
{
   #region Fields & Properties

   public Button button;
   [SerializeField] Image iconImage;
   [SerializeField] GameObject takenOverlay;

   public bool IsDiabled {  get; private set; }

   [SerializeField] RoomPlayer player;
   FarmerSO farmer;

   #endregion

   #region Unity Callbacks

   //void Awake() 
   //{
   //	
   //}

   //void Start() 
   //{
   //	
   //}

   //void Update() 
   //{
   //	
   //}
   #endregion

   public void SetupButton(RoomPlayer _player, FarmerSO _farmer)
   {
      player = _player;
      farmer = _farmer;
      iconImage.sprite = _farmer.Icon;
   }

   public GameObject GetTakenOverlay()
   {
      return takenOverlay;
   }

   public FarmerSO GetFarmer()
   {
      return farmer;
   }

   public void OnButtonClick()
   {
      player.SetSelectedFarmer(farmer);
      player.lockInButton.interactable = true;

      if (!player.IsSpawnedIn)
      {
         player.IntroInstance = Instantiate(player.IntroPrefab, player.IntroSpawnPoint);
         player.IsSpawnedIn = true;
      }

      UpdateIntroMaterial(farmer);
   }

   void UpdateIntroMaterial(FarmerSO farmer)
   {
      Renderer[] renderers = player.IntroInstance.GetComponentsInChildren<Renderer>();

      foreach (Renderer renderer in renderers)
      {
         if (renderer.name == "tractor")
         {
            renderer.material.SetTexture("_BaseMap", player.IntroTextures[GetIndexFromFarmer(farmer.Name)]);
         }
      }
   }

   public void SetDisabled()
   {
      IsDiabled = true;
      takenOverlay.SetActive(true);
      button.interactable = false;
   }

   public int GetIndexFromFarmer(string farmer)
   {
      switch (farmer)
      {
         case IFG.Ron:
            return 0;
            case IFG.Janis:
            return 1;
            case IFG.Jerry:
            return 2;
         case IFG.Ric:
            return 3;
            case IFG.Becky:
            return 4;
            case IFG.Mike:
            return 5;
         default:
            Debug.LogWarning($"FUBAR farmer: {farmer}");
            return -1;
      }
   }
}

