using Mirror;
using UnityEngine;

public class PlayerMove : NetworkBehaviour
{
   #region Fields & Properties


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
      if(!isOwned) return;

      if (Input.GetKeyDown(KeyCode.Space))
      {
         transform.Translate(Vector3.forward);
         //Debug.Log("Moving");
      }
   }
   #endregion

}

