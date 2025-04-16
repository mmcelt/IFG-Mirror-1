using Mirror;
using System.Collections;
using UnityEngine;

public class Range : NetworkBehaviour
{
   #region Fields & Properties

   [SerializeField] float spawmDelayModifier = 1.5f;

   string farmer;
   bool isDoubled;

   #endregion

   #region Mirror Callbacks


   #endregion

   public void Initialize(string _farmer, bool _isDoubled)
   {
      farmer = _farmer;
      isDoubled = _isDoubled;

      //handle doubled
      if (isDoubled)
      {
         foreach (Transform child in transform)
         {
            if (child.name == "Text")
               child.localRotation = Quaternion.Euler(90, 90, 0);
         }
      }
      else
      {
         foreach (Transform child in transform)
         {
            if (child.name == "Text")
               child.localRotation = Quaternion.Euler(90, 0, 0);
         }
      }

      //handle cow coloring
      foreach (Transform child in transform.GetComponentsInChildren<Transform>())
      {
         if (child.name == "Farm Cow")
         {
            if (child.TryGetComponent(out SkinnedMeshRenderer renderer))
            {
               //renderer.materials = new Material[] { StickerManager.Instance.GetCowMaterial(farmer) };
            }
         }
      }

      StartCoroutine(StaggerCowsRoutine());
   }

   IEnumerator StaggerCowsRoutine()
   {
      Animator[] cowAnims = GetComponentsInChildren<Animator>();

      foreach (var anim in cowAnims)
      {
         anim.enabled = false;
      }

      foreach (var anim in cowAnims)
      {
         yield return new WaitForSeconds(Random.value * spawmDelayModifier);
         anim.enabled = true;
      }
   }
}

