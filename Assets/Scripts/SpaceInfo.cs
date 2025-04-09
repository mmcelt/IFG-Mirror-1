using DG.Tweening;
using UnityEngine;

public class SpaceInfo : MonoBehaviour
{
   #region Fields & Properties

   [SerializeField] LayerMask spaceLayer;

   string clickedObjectName;
   GameObject clickedObject, selectedObject;

   bool inprogress;

   #endregion

   #region Unity Callbacks

   void Update()
   {
      if (Input.GetMouseButton(1) && !inprogress) // Check for right mouse button down
      {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Create ray from mouse position

         RaycastHit hit;

         if (Physics.Raycast(ray, out hit, Mathf.Infinity, spaceLayer)) // Check if ray hits anything
         {

            clickedObject = hit.collider.gameObject; // Get the clicked object

            if (!clickedObject.TryGetComponent(out SpaceInfo info)) return;

            selectedObject = hit.collider.gameObject;
            clickedObjectName = clickedObject.name;

            Debug.Log(clickedObjectName);

            // Perform actions based on the clicked object
            ShowSpaceInfoPanel();
            inprogress = true;
         }
      }
      else if (Input.GetMouseButtonUp(1))
      {
         if (selectedObject == null) return;

         UIManager.Instance.spaceInfoPanel.GetComponent<DOTweenAnimation>().DOPlayBackwards();
         inprogress = false;
         selectedObject = null;
      }
   }

   void ShowSpaceInfoPanel()
   {
      //BoardManager.Instance.ShowSpace(int.Parse(clickedObjectName), true);
   }
   #endregion
}

