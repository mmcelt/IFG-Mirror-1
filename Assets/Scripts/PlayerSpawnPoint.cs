using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
   #region Fields & Properties


   #endregion

   #region Unity Callbacks

   void Awake()
   {
      //PlayerSpawnSystem.AddSpawnPoint(transform);
   }

   void OnDestroy()
   {
      //PlayerSpawnSystem.RemoveSpawnPoint(transform);
   }

   //void OnDrawGizmos()
   //{
   //   Gizmos.color = Color.blue;
   //   Gizmos.DrawSphere(transform.position, 1f);
   //   Gizmos.color = Color.green;
   //   Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2f);
   //}
   #endregion

}

