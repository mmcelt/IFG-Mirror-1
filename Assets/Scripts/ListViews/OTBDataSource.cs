using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class OTBDataSource : MonoBehaviour
{
   #region Fields & Properties

   [SerializeField] MagicList magicList;

   PlayerManager pManager;

   List<OTBCard> otbCardList;

   public ObservableCollection<OTBCard> OtbCards;

   #endregion

   #region Unity Callbacks

   void OnEnable()
   {

      if (!pManager)
         pManager = UIManager.Instance.GetMyPlayer();

      ReloadData();
   }

   //void Start()
   //{

   //}
   #endregion

   public void ReloadData()
   {
      if (!pManager)
         Debug.LogError("NO pMANAGER");

      if (pManager != null)
      {
         //otbCardList = pManager.myOTBs;
         otbCardList = otbCardList.OrderBy(x => x.summary).ToList();

         OtbCards = new ObservableCollection<OTBCard>(otbCardList);
         magicList.SetData(OtbCards);
      }
   }

   public void UpdatePlayerManagerOtbs()
   {
      //Debug.Log($"Otb Cards: {DeckManager.Instance.otbCards.Count}");
      //Debug.Log($"In DS.UPMO: {FinanceManager.Instance.GetSelectedOtbCard().cardNumber}");
      //pManager.ReturnOTBCard(FinanceManager.Instance.GetSelectedOtbCard());
      //OtbCards.Remove(FinanceManager.Instance.GetSelectedOtbCard());

      //pManager.myOTBs.Clear();
      //pManager.myOTBs.AddRange(OtbCards);
      //Debug.Log($"MyOtbs Count: {pManager.myOTBs.Count}");
      //UIManager.Instance.UpdateMyOtbCountText(pManager.myOTBs.Count);
   }
}

