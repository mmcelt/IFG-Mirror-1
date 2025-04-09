using Mirror;
using UnityEngine;

public class SingletonMirror<T> : NetworkBehaviour where T : MonoBehaviour
{
   #region Fields & Properties

   static T _instance;

   public static T Instance
   {
      get
      {
         return _instance;
      }
   }
   #endregion

   #region Unity Callbacks

   protected virtual void Awake()
   {
      if (_instance == null)
         _instance = this as T;
      else
      {
         Destroy(gameObject);
      }
   }
   #endregion

}

