using Mirror;
using System.Collections;
using UnityEngine;

public class GameManager : SingletonMirror<GameManager>
{
   #region Fields & Properties

   MyNetworkManager room;

   public MyNetworkManager Room
   {
      get
      {
         if (room != null) return room;
         return room = NetworkManager.singleton as MyNetworkManager;
      }
   }

   #endregion

   #region Unity Callbacks

   //void Start() 
   //{
   //	
   //}

   //void Update() 
   //{
   //	
   //}
   #endregion

   #region Mirror Callbacks

   public override void OnStartServer()
   {
      StartCoroutine(GameLoopRoutine());
   }
   #endregion

   #region Server

   IEnumerator GameLoopRoutine()
   {
      yield return new WaitForSeconds(0.1f);
      //yield return null;
      yield return StartCoroutine(PregameSetupRoutine());
      
   }

   IEnumerator PregameSetupRoutine()
   {
      DeckManager.Instance.InitializeDecks();
      GiveInitialOTBsToPlayers();
      PlaceInitialBoardStickers();

      yield return null;
   }

   void GiveInitialOTBsToPlayers()
   {
      for (int i = 0; i < 2; i++)
      {
         foreach (var player in Room.GamePlayers)
         {
            Debug.Log($"In Give OTB's: {player.name}");
            DeckManager.Instance.DrawOTBCard(player.gameObject);
         }
      }
   }

   void PlaceInitialBoardStickers()
   {

   }
   #endregion

   #region Client


   #endregion

}

