using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
   #region Fields & Properties

   [SerializeField] Vector3 movement;

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

   [ClientCallback]
   void Update()
   {
      if (!isOwned) return;
      if (!Input.GetKeyDown(KeyCode.Space)) return;

      //transform.Translate(movement);

      CmdMove();
   }

   #endregion

   [Command]
   void CmdMove()
   {
      //validation logic here...

      RpcMove();
   }

   [ClientRpc]
   void RpcMove()
   {
      transform.Translate(movement);
   }
}

