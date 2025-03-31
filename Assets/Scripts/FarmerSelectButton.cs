using UnityEngine;
using UnityEngine.UI;

public class FarmerSelectButton : MonoBehaviour
{
   #region Fields & Properties

   [SerializeField] Image iconImage;
   [SerializeField] GameObject takenOverlay;

   RoomPlayer player;
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

   public void OnButtonClick()
   {
      player.SetSelectedFarmer(farmer);
   }
}

