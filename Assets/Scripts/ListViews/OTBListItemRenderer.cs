using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OTBListItemRenderer : MonoBehaviour, IListItemRenderer<OTBCard>
{
   #region Fields & Properties

   [SerializeField] TMP_Text itemText;
   [SerializeField] Button button;

   OTBDataSource dataSource;
   OTBCard selectedCard;
   int index;

   public static Action<int> OnOtbCardIndexSelected;
   public static Action<OTBCard> OtbCardSelected;

   #endregion

   #region Unity Callbacks

   //void Start() 
   //{
   //	
   //}

   #endregion

   public void BindView(OTBCard value)
   {
      itemText.text = $"{value.summary}-{value.cardNumber}";
      selectedCard = value;
      dataSource = GetComponentInParent<OTBDataSource>();
      index = dataSource.OtbCards.IndexOf(selectedCard);
   }

   public void OnButtonClick()
   {
      Debug.Log($"Clicked Button at Index: {index} with value: {selectedCard.cardNumber}");

      OnOtbCardIndexSelected?.Invoke(index);
      OtbCardSelected?.Invoke(selectedCard);
   }

   public OTBDataSource GetOTBDataSource()
   {
      return dataSource;
   }

   public OTBCard GetSelectedCard()
   {
      return selectedCard;
   }

   public int GetIndex()
   {
      return index;
   }
}

