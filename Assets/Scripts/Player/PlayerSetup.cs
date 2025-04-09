using Mirror;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
   #region Fields & Properties

   [SerializeField] GameObject playerUI;

   #endregion

   #region Mirror Callbacks

   public override void OnStartAuthority()
   {
      playerUI.SetActive(true);
   }
   #endregion

}

