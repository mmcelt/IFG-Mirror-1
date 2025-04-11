using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
   #region Fields

   static T instance;

   public static T Instance
   {
      get
      {
         return instance;
      }
   }
   #endregion

   #region Unity Methods

   protected virtual void Awake()
   {
      if (instance == null)
         instance = this as T;
      else
      {
         Destroy(gameObject);
      }
   }
   #endregion
   //TO USE THIS CLASS: public class UIManager : Singleton<UIManager>
}


 
