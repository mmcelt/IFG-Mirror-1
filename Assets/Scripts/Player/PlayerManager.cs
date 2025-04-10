using Mirror;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
   #region Fields & Properties

   [SerializeField] GamePlayer player;

   #endregion

   #region Mirror Callbacks

   public override void OnStartAuthority()
   {
      
   }
   #endregion

}

