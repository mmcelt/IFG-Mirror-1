using DG.Tweening;
using Mirror;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
   #region Fields & Properties

   [Header("LEFT PANEL")]
   [SerializeField] TMP_Text playerNameText;
   [SerializeField] TMP_Text farmerNameText;
   [SerializeField] GameObject[] badNews;
   [SerializeField] TMP_Text[] opNames, opCash, opNotes, opOtbs;
   public Button rollButton, endTurnButton;
   [SerializeField] TMP_Text playerOtbText, playerCashText, playerNotesText, playerNetworthText;
   [SerializeField] Image tractorImage, harvesterImage;
   [SerializeField] TMP_Text hayDoubledCounterText;
   [SerializeField] Button actionsPanelButton;
   [SerializeField] Image hayImage, grainImage, fruitImage, spudsImage, fCowImage, rCowImage;
   [SerializeField] Sprite hayNSprite, hayDSprite, grainNSprite, grainDSprite, spudsNSprite, spudsDSprite, cowNSprite, cowDSprite;
   [SerializeField] TMP_Text hayText, grainText, fruitText, spudsText, fCowsText, rCowsText;

   [Header("RIGHT PANEL")]
   public TMP_Text timerText;
   public TMP_Text gameTypeText, gameLengthText;
   public TMP_Text[] opNetworthNames, opNetworthNetworths;
   public TMP_Text activePlayerText;
   [Header("-Decks")]
   public Image otbDeck;
   public TMP_Text otbDeckText, otbShuffleText;
   public Image oeDeck;
   public TMP_Text oeDeckText, oeShuffleText;
   public Image ffDeck;
   public TMP_Text ffDeckText, ffShuffleText;
   [Header("-Misc")]
   public Button optionsButton;
   [Header("CENTER PANELS")]
   [Header("-Boardspace Panel")]
   public GameObject boardSpacePanel;
   public TMP_Text headerText, spaceText;
   public Image[] borders;
   [Header("-Space Info Panel")]
   public GameObject spaceInfoPanel;
   public TMP_Text spaceInfoHeaderText;
   public TMP_Text spaceInfoText;
   [Header("-Harvest Panels")]
   public GameObject harvestPanel;
   public Image panelImage;
   public TMP_Text harvestCheckText, adjustedCheckText, expensesText, netCheckText;
   public Button harvestRollButton, harvestOkButton;
   public GameObject garnishedHarvestPanel, garnishedResultPanel;
   public TMP_Text garnishedResultsText;
   [Header("-OTB Card")]
   public GameObject otbCardPanel;
   public TMP_Text otbDescriptionText, otbTotalText;
   [Header("-OE Card")]
   public GameObject oePanel;
   public TMP_Text oeDescriptionText, oeExpenseText;
   [Header("-FF Card Local")]
   public GameObject ffCardLocalPanel;
   public TMP_Text ffCardLocalText;
   [Header("-FF Card Remote")]
   public GameObject ffCardRemotePanel;
   public TMP_Text ffCardRemoteText;
   [Header("-Actions Panel")]
   public GameObject actionsPanel;
   public TMP_Text apCashText, apNotesText;
   [Header("OTB Stuff")]
   [SerializeField] MagicList otbMagicListView;
   //public OTBDataSource otbDataSource;
   //[SerializeField] OTBListView otbListView;
   public TMP_InputField downPaymentInput;
   public Button buyOtbButton, payInFullButton, sellToPlayerButton, sellToBankButton;
   public TMP_InputField repayLoanInput, getLoanInput;
   public Button repayLoanButton, getLoanButton;
   [Header("Modal Panels")]
   public GameObject buttonsModalPanel;
   [Header("MISC")]
   [SerializeField] GamePlayer myPlayer;
   [SerializeField] Tractor myTractor;
   //[SerializeField] InputFieldManager ifManager;
   [SerializeField] Material normalMat, outlineMat;
   public PlayerManager pManager;
   [SerializeField] PlayerMove pMove;

   MyNetworkManager room;
   MyNetworkManager Room
   {
      get
      {
         if (room != null) return room;

         return room = NetworkManager.singleton as MyNetworkManager;
      }
   }

   #endregion

   #region Unity Callbacks

   void Start()
   {
      StartCoroutine(SetMyPlayersRoutine());
      Invoke(nameof(StartRemotePlayerUpdating), 0.5f);
   }

   #endregion

   #region Getters/Setters

   public void SetPlayerName(string playerName)
   {
      playerNameText.text = playerName;
   }

   public void SetFarmerName(string farmerName)
   {
      farmerNameText.text = farmerName;
      farmerNameText.color = IFG.GetColorFromFarmer(farmerName);
   }

   IEnumerator SetMyPlayersRoutine()
   {
      yield return new WaitForSeconds(0.35f);
      myPlayer = GetComponentInParent<GamePlayer>();
      pManager = myPlayer.GetComponent<PlayerManager>();
      myTractor = myPlayer.GetMyTractor();
      Debug.Log($"My Trctor: {myTractor}");
      pMove = myTractor.GetComponent<PlayerMove>();
   }

   public PlayerManager GetMyPlayer()
   {
      return myPlayer.GetComponent<PlayerManager>();
   }
   #endregion

   #region UI Callbacks

   public void OnTestButtonClick()
   {
      pMove.InitMove(1);
   }

   public void OnRollButtonClick()
   {
      if (actionsPanel.activeSelf)
         actionsPanel.SetActive(false);

      //RollManager.Instance.Roll("move");
   }

   public void OnEndTurnButtonClick()
   {
      //pManager.EndTurn();
   }

   public void OnActionsPanelButtonClick()
   {
      actionsPanel.SetActive(!actionsPanel.activeSelf);

      if (actionsPanel.activeSelf)
         repayLoanInput.Select();
   }

   public void OnBoardspacePanelClick()
   {
      //BoardManager.Instance.OnPanelClicked();
   }

   public void OnBoardspacePanelRewind()
   {
      //BoardManager.Instance.OnRewind();
   }

   public void OnOtbCardPanelClick()
   {
      //pManager.isOkToCloseOtbPanel = true;
   }

   public void OnOtbCardPanelRewind()
   {
      //Debug.Log("In UI.OCPR");
      otbCardPanel.SetActive(false);
   }

   public void OnFfLocalCardPanelClick()
   {
      Debug.Log("CLICKED LFF Panel");
      //pManager.isOkToCloseLocalFFPanel = true;
      //ffCardLocalPanel.GetComponent<DOTweenAnimation>().DOPlayBackwards();
   }

   public void OnFfLocalCardRewind()
   {
      ffCardLocalPanel.SetActive(false);
   }

   public void OnFfRemoteCardPanelClick()
   {
      Debug.Log("CLICKED RFF Panel");
      //pManager.isOkToCloseRemoteFFPanel = true;
      ffCardRemotePanel.GetComponent<DOTweenAnimation>().DOPlayBackwards();
      ffCardRemotePanel.transform.DOLocalMove(new Vector3(629, -256), 0.5f);
   }

   public void OnFfRemoteCardRewind()
   {
      ffCardRemotePanel.SetActive(false);
   }

   public void OnHarvestPanelRollButtonClick()
   {
      //HarvestManager.Instance.DoTheHarvestRoll();
   }

   public void OnHarvestPanelOkButtonClick()
   {
      //HarvestManager.Instance.isOkToCloseInfoPanel = true;
      harvestPanel.GetComponent<DOTweenAnimation>().DOPlayBackwards();
   }

   public void OnHarvestPanelRewind()
   {
      harvestPanel.SetActive(false);
   }

   public void OnOeCardPanelClick()
   {
      //HarvestManager.Instance.isOkToCloseOeCardPanel = true;
      //HarvestManager.Instance.isOkToCloseOeCardPanelAgain = true;
   }

   public void OnOeCardRewind()
   {
      oePanel.SetActive(false);
   }

   public void OnGarnishedHarvestPanelClick()
   {
      //HarvestManager.Instance.isOkToCloseGarnishedPanel = true;
      garnishedHarvestPanel.GetComponent<DOTweenAnimation>().DOPlayBackwards();
   }

   public void OnGarnishedHarvestRewind()
   {
      garnishedHarvestPanel.SetActive(false);
   }

   public void OnGarnishedResultsPanelClick()
   {
      //HarvestManager.Instance.isOkToCloseGarnishedResultsPanel = true;
      garnishedResultPanel.GetComponent<DOTweenAnimation>().DOPlayBackwards();
   }

   public void OnGarnishedResultsRewind()
   {
      garnishedResultPanel.SetActive(false);
   }

   public void OnRepayLoanButtonClick()
   {

   }

   public void OnGetLoanButtonClick()
   {

   }

   public void OnDownPaymentInputValueChanged(string value)
   {
      int dp = int.Parse(value);
      //FinanceManager.Instance.SetDownPayment(dp);
   }

   public void OnBuyOtbButtonClick()
   {
      //if (!pManager.IsMyTurn) return;
      if (pMove.currentSpace > 14) return;

      //FinanceManager.Instance.OkToPurchaseOtb = true;
      actionsPanel.SetActive(false);
   }
   #endregion

   #region Updaters

   void StartRemotePlayerUpdating()
   {
      //Debug.Log("In RU Call");
      if (Room.GamePlayers.Count > 1)
         StartCoroutine(UpdateRemotePlayerNamesRoutine());
   }

   IEnumerator UpdateRemotePlayerNamesRoutine()
   {
      for (int i = 0; i < 5; i++)
      {
         Debug.Log("IN URPI ROUTINE");

         int index = 0;
         foreach (var player in Room.GamePlayers)
         {
            if (player.GetDisplayName() != myPlayer.GetDisplayName())
            {
               Debug.Log($"In IF: {player.GetDisplayName()}");

               opNames[index].text = player.GetDisplayName();
               opNetworthNames[index].text = player.GetDisplayName();

               if (player.GetFarmerName() != null)
               {
                  string farmer = player.GetFarmerName();
                  opNames[index].color = IFG.GetColorFromFarmer(farmer);
                  opNetworthNames[index].color = IFG.GetColorFromFarmer(farmer);

                  switch (farmer)
                  {
                     case IFG.BECKY:
                        opNames[index].fontMaterial = SetFontOutline(true);
                        opNetworthNames[index].fontMaterial = SetFontOutline(true);
                        break;
                     case IFG.MIKE:
                        opNames[index].fontMaterial = SetFontOutline(true);
                        opNetworthNames[index].fontMaterial = SetFontOutline(true);
                        break;
                  }
               }
               index++;
            }
         }

         foreach (var player in Room.GamePlayers)
         {
            Debug.Log($"UIM: {player.GetDisplayName()}");
         }

         yield return new WaitForSeconds(0.5f);
      }
   }

   public void UpdateBadNews(int index, bool state)
   {
      badNews[index].SetActive(state);
   }

   public void UpdateOtherPlayersNameTexts(int index, string name)
   {
      //TODO: Remote updating
      opNames[index].text = name;
   }

   public void UpdateMyOtbCountText(int count)
   {
      playerOtbText.text = $"OTB's: {count}";
   }

   public void UpdateMyCashText(int amount)
   {
      playerCashText.text = amount.ToString();
   }

   public void UpdateMyNotesText(int amount)
   {
      playerNotesText.text = amount.ToString();
   }

   public void UpdateMyNetworthText(int amount)
   {
      playerNetworthText.text = amount.ToString();
   }

   public void UpdateHarvester(bool harvester)
   {
      harvesterImage.color = harvester ? Color.white : IFG.GreyedOut;
   }

   public void UpdateTractor(bool tractor)
   {
      tractorImage.color = tractor ? Color.white : IFG.GreyedOut;
   }

   public void UpdateCommodityStickers(bool hayD, bool grainD, bool spudsD, bool cowsI)
   {
      hayImage.sprite = hayD ? hayDSprite : hayNSprite;
      grainImage.sprite = grainD ? grainDSprite : grainNSprite;
      spudsImage.sprite = spudsD ? spudsDSprite : spudsNSprite;
      fCowImage.sprite = cowsI ? cowDSprite : cowNSprite;
      rCowImage.sprite = cowsI ? cowDSprite : cowNSprite;
   }

   public void UpdateMyCommodityAmounts(int hayCounter, int hay, int grain, int fruit, int spuds, int fCows, int rCows)
   {
      //Debug.Log($"IN UCA: {fruit}");

      hayDoubledCounterText.text = $"x{hayCounter}";
      hayText.text = $"{hay}";
      grainText.text = $"{grain}";
      fruitText.text = $"{fruit}";
      spudsText.text = $"{spuds}";
      fCowsText.text = $"{fCows}";
      rCowsText.text = $"{rCows}";
   }

   public void UpdateMyEquipment(bool tractor, bool harvester)
   {
      tractorImage.color = tractor ? Color.white : IFG.GreyedOut;
      harvesterImage.color = harvester ? Color.white : IFG.GreyedOut;
   }

   #endregion

   //void OnOtbCardSelected(OTBCard card)
   //{
   //   Debug.Log($"In UM.OOCS");

   //   if (pManager.IsMyTurn)
   //   {
   //      sellToBankButton.interactable = true;
   //      sellToPlayerButton.interactable = true;
   //   }
   //   else
   //   {
   //      sellToBankButton.interactable = false;
   //      sellToPlayerButton.interactable = false;
   //   }
   //}

   public Material SetFontOutline(bool state)
   {
      if (state)
         return outlineMat;
      else
         return normalMat;
   }

}
