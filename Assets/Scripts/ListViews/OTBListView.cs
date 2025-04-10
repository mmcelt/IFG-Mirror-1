using System;
using System.Collections;
using UnityEngine;

public class OTBListView : MonoBehaviour
{
   #region Fields & Properties

   [SerializeField] OTBDataSource dataSource;

   int selectedIndex;
   OTBCard selectedCard;
   bool isOkToUpdatePlayerManager;

   //public static Action OnBuyOtbComplete;

   #endregion

   #region Unity Callbacks

   void OnEnable()
   {
      OTBListItemRenderer.OnOtbCardIndexSelected += HandleOtbCardSelected;
      //FinanceManager.OnOtbActionCompleted += HandleOtbActionCompleted;
   }

   void OnDisable()
   {
      OTBListItemRenderer.OnOtbCardIndexSelected -= HandleOtbCardSelected;
      //FinanceManager.OnOtbActionCompleted -= HandleOtbActionCompleted;
   }
   #endregion

   public OTBCard GetSelectedCard()
   {
      return selectedCard;
   }

   //public void OnBuyOtbButtonClicked()  //called by the Buy otb button
   //{
   //   //yield return StartCoroutine(FinanceManager.Instance.PreBuyOtbRoutine());


   //   //Debug.Log($"B4 remove card otbCount: {dataSource.OtbCards.Count}");
   //   //dataSource.OtbCards.RemoveAt(selectedIndex);
   //   //Debug.Log($"Afer remove card at index: {selectedIndex}::otbCount: {dataSource.OtbCards.Count}");

   //   //FinanceManager.Instance.OkToUpdatePlayerManager = true;
   //}

   void HandleOtbCardSelected(int index)
   {
      selectedIndex = index;
      selectedCard = dataSource.OtbCards[index];
      UIManager.Instance.downPaymentInput.text = $"{(int)(selectedCard.totalCost * 0.2f)}";
      UIManager.Instance.downPaymentInput.Select();
   }

   //void HandleOtbActionCompleted()  //preformed at end of buying
   //{
   //   Debug.Log("In OTBLV.HOAC");
   //   dataSource.UpdatePlayerManagerOtbs();
   //}

   //void HandleBuyOtbCompleted()
   //{
   //   Debug.Log("In RENDERER.Handle Buy Completed");
   //   isOkToUpdatePlayerManager = true;
   //   Debug.Log($"IOTUPM? {isOkToUpdatePlayerManager}");
   //}

}

