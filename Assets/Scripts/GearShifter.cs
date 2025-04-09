using UnityEngine;

public class GearShifter : MonoBehaviour
{
   #region Fields & Properties

   [SerializeField] Animator anim;

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

   //void Update() 
   //{
   //	
   //}
   #endregion

   void StopBackupAlarm()
   {
      Debug.Log($"In SBA: {anim.GetBool("Forward")}");

      if (anim.GetBool("Forward"))
      {
         //AudioManager3D.Instance.Sources[21].Stop();

      }
      else if (anim.GetBool("Reverse"))
      {

      }
   }
}

