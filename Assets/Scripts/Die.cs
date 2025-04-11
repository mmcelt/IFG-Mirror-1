using UnityEngine;

public class Die : MonoBehaviour
{
   #region Fields & Properties

   [SerializeField] AudioSource aSource;

   #endregion

   #region Unity Callbacks

   void OnCollisionEnter(Collision collision)
   {
      aSource.Stop();
      aSource.Play();
   }
   #endregion

}

