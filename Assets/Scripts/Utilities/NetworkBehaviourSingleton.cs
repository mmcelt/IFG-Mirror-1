using Mirror;
using UnityEngine;

public class NetworkBehaviourSingleton<T> : NetworkBehaviour where T : Component
{
   public static T Instance { get; private set; }

   public virtual void Awake()
   {
      if (Instance == null)
      {
         Instance = this as T;
         //DontDestroyOnLoad(this);
      }
      else
      {
         Destroy(gameObject);
      }
   }
}

