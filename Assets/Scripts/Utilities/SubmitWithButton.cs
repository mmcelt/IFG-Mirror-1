using TMPro;
using UnityEngine;

public class SubmitWithButton : MonoBehaviour
{
   #region Fields & Properties

   public string submitKey = "Submit";
   public bool trimWhitespace = true;
   //public UIManager uiManager;
   UIManager uiManager;      //serialized for debugging
   //FinanceManager fManager;  //serialized for debugging

   TMP_InputField inputField;

   #endregion

   #region Unity Callbacks

   //void Awake() 
   //{
   //	
   //}

   void Start()
   {
      inputField = GetComponent<TMP_InputField>();

      inputField.onEndEdit.AddListener(fieldValue =>
      {
         if (trimWhitespace)
            inputField.text = fieldValue = fieldValue.Trim();
         if (Input.GetButton(submitKey))
            OnValidateAndSubmit(fieldValue);
      });

      //fManager = FinanceManager.Instance;
      uiManager = UIManager.Instance;
   }
   #endregion

   bool isInvalid(string fieldValue)
   {
      // change to the validation you want
      return SelectCorrectValidation(inputField.name);
   }
   void OnValidateAndSubmit(string fieldValue)
   {
      if (isInvalid(fieldValue))
         return;

      // change to the submit code
      SelectCorrectAction(inputField.name);
   }
   // to be called from a submit button onClick event
   public void ValidateAndSubmit()
   {
      OnValidateAndSubmit(inputField.text);
   }

   void SelectCorrectAction(string target)
   {
      switch (target)
      {
         //TODO: IMPLEMENT SUBMIT W/BUTTON
         //case "PN Input":
         //   MyNetworkManager.Instance.OnLoginButtonClicked();
         //   break;
         case "RL Input":
            uiManager.OnRepayLoanButtonClick();
            break;

         case "GL Input":
            uiManager.OnGetLoanButtonClick();
            break;

         case "DP Input":
            uiManager.OnBuyOtbButtonClick();
            break;

         //case "FL Input":
         //   uiManager.OnGetForcedLoanButtonClick();
         //   break;

         //case "Sale Price Input":
         //   uiManager.OnDoOtbSaleToPlayerButtonClicked();
         //   break;
      }
      //uiManager.actionsPanel.SetActive(false);
      //uiManager.actionsPanel.GetComponent<DOTweenAnimation>().DOPlayBackwards();
   }

   bool SelectCorrectValidation(string target)
   {
      //Debug.Log("IN Validation " + target);
      switch (target)
      {
         //case "PN Input":
         //   if (!NetworkManager3D.Instance.GetLoginButton())
         //      return true;
         //   break;
         case "RL Input":
            if (!uiManager.repayLoanButton.interactable)
               return true;
            break;

         case "GL Input":
            if (!uiManager.getLoanButton.interactable)
               return true;
            break;

         case "DP Input":
            if (!uiManager.buyOtbButton.interactable)
               return true;
            break;

         //case "FL Input":
         //   if (!uiManager.getForcedLoanButton.interactable)
         //      return true;
         //   break;

         //case "Sale Price Input":
         //   if (!uiManager.sellTheOtbToPlayerButton.interactable)
         //      return true;
         //   break;
      }
      return false;
   }

}

