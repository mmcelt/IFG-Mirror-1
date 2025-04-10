using Mirror;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
   #region Fields & Properties

   [SerializeField] GameObject playerUI;
   [SerializeField] GamePlayer player;

   #endregion

   #region Mirror Callbacks

   public override void OnStartAuthority()
   {
      playerUI.SetActive(true);
      UIManager.Instance.SetPlayerName(player.GetDisplayName());
      UIManager.Instance.SetFarmerName(player.GetFarmerName());
   }
   #endregion

}

