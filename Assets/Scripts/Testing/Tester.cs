using UnityEngine;

public class Tester : MonoBehaviour
{
   #region Fields & Properties

   [SerializeField] Transform holder;
   [SerializeField] TestPrefab testPrefab;

   #endregion

   #region Unity Callbacks

   void Awake()
   {
      for (int i = 0; i < 6; i++)
      {
         TestPrefab go = Instantiate(testPrefab, holder);
      }
   }

   void Start()
   {
      Transform[] children = holder.GetComponentsInChildren<Transform>();

      Debug.Log(children.Length);
   }

   //void Update() 
   //{
   //	
   //}
   #endregion


}

