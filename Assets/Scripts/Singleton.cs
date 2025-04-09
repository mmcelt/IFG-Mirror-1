using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
   #region Fields

   static T _instance;

   public static T Instance
   {
      get
      {
         return _instance;
      }
   }
   #endregion

   #region Unity Methods

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
   //TO USE THIS CLASS: public class UIManager : Singleton<UIManager>
}


 
