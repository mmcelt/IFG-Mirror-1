using Mirror;

public class SingletonMirror<T> : NetworkBehaviour where T : NetworkBehaviour
{
   #region Fields & Properties

   static T instance;

   public static T Instance
   {
      get
      {
         return instance;
      }
   }
   #endregion

   #region Unity Callbacks

   protected virtual void Awake()
   {
      if (instance == null)
         instance = this as T;
      else
      {
         //Destroy(gameObject);
      }
   }
   #endregion

}

