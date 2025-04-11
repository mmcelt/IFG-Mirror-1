using Mirror;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class OTBCard
{
   public int cardNumber;
   public string description;
   public string summary;
   public int totalCost;
   public bool bottomCard;

}

public class FFCard
{
   public int cardNumber;
   public string description;
   public bool bottomCard;
}

public class OECard
{
   public int cardNumber;
   public string description;
   public bool bottomCard;
}

public class DeckManager : SingletonMirror<DeckManager>
{
   #region Fields & Properties

   List<OTBCard> otbCards;
   List<FFCard> ffCards;
   List<OECard> oeCards;

   [SerializeField] int otbDeckShuffleCounter = -1;
   [SerializeField] int ffDeckShuffleCounter = -1;
   [SerializeField] int oeDeckShuffleCounter = -1;

   #endregion

   #region Mirror Callbacks


   #endregion

   [Server]
   public void InitializeDecks()
   {
      if (!isServer) return;

      otbCards = new List<OTBCard>();
      ffCards = new List<FFCard>();
      oeCards = new List<OECard>();

      MakeTheOtbDeck();
      //MakeTheFfDeck();
      //MakeTheOeDeck();
   }

   void MakeTheOtbDeck()   //46 cards
   {
      CreateAnOTBCard(
   01,
      "<color=yellow>NEIGHBORING FARMER SELLS OUT</color>\n<b>10 acres of Grain</b> at 2000 per acre",
      "10 acres GRAIN - Total $20,000", 20000);
      CreateAnOTBCard(
         02,
      "<color=yellow>NEIGHBORING FARMER SELLS OUT</color>\n<b>10 acres of Grain</b> at 2000 per acre",
      "10 acres GRAIN - Total $20,000", 20000);
      CreateAnOTBCard(
         03,
      "<color=yellow>NEIGHBORING FARMER SELLS OUT</color>\n<b>10 acres of Grain</b> at 2000 per acre",
      "10 acres GRAIN - Total $20,000", 20000);
      CreateAnOTBCard(
         04,
      "<color=yellow>NEIGHBORING FARMER SELLS OUT</color>\n<b>10 acres of Grain</b> at 2000 per acre",
      "10 acres GRAIN - Total $20,000", 20000);
      CreateAnOTBCard(
         05,
      "<color=yellow>NEIGHBORING FARMER SELLS OUT</color>\n<b>10 acres of Grain</b> at 2000 per acre",
      "10 acres GRAIN - Total $20,000", 20000);
      CreateAnOTBCard(
         06,
         "<color=#B27738>LIVESTOCK AUCTION</color>\n<b>10 pregnant cows</b> at 500 each",
         "10 Cows - Total $5,000", 5000);
      CreateAnOTBCard(
         07,
         "<color=#B27738>LIVESTOCK AUCTION</color>\n<b>10 pregnant cows</b> at 500 each",
         "10 Cows - Total $5,000", 5000);
      CreateAnOTBCard(
         08,
         "<color=#B27738>LIVESTOCK AUCTION</color>\n<b>10 pregnant cows</b> at 500 each",
         "10 Cows - Total $5,000", 5000);
      CreateAnOTBCard(
         09,
         "<color=#B27738>LIVESTOCK AUCTION</color>\n<b>10 pregnant cows</b> at 500 each",
         "10 Cows - Total $5,000", 5000);
      CreateAnOTBCard(
         10,
         "<color=#B27738>LIVESTOCK AUCTION</color>\n<b>10 pregnant cows</b> at 500 each",
         "10 Cows - Total $5,000", 5000);
      CreateAnOTBCard(
         11,
         "<color=#B27738>LIVESTOCK AUCTION</color>\n<b>10 pregnant cows</b> at 500 each",
         "10 Cows - Total $5,000", 5000);
      CreateAnOTBCard(
         12,
         "<color=green>NEIGHBORING FARMER SELLS OUT</color>\n<b>10 acres of Hay</b> at 2000 per acre",
         "10 acres HAY - Total $20,000", 20000);
      CreateAnOTBCard(
         13,
         "<color=green>NEIGHBORING FARMER SELLS OUT</color>\n<b>10 acres of Hay</b> at 2000 per acre",
         "10 acres HAY - Total $20,000", 20000);
      CreateAnOTBCard(
         14,
         "<color=green>NEIGHBORING FARMER SELLS OUT</color>\n<b>10 acres of Hay</b> at 2000 per acre",
         "10 acres HAY - Total 20,000", 20000);
      CreateAnOTBCard(
         15,
         "<color=green>NEIGHBORING FARMER SELLS OUT</color>\n<b>10 acres of Hay</b> at 2000 per acre",
         "10 acres HAY - Total $20,000", 20000);
      CreateAnOTBCard(
         16,
         "<color=green>NEIGHBORING FARMER SELLS OUT</color>\n<b>10 acres of Hay</b> at 2000 per acre",
         "10 acres HAY - Total $20,000", 20000);
      CreateAnOTBCard(
         17,
         "<color=orange>EQUIPMENT SALE</color>\nold but usable <b>TRACTOR</b>",
         "TRACTOR - Total $10,000", 10000);
      CreateAnOTBCard(
         18,
         "<color=orange>EQUIPMENT SALE</color>\nold but usable <b>TRACTOR</b>",
         "TRACTOR - Total $10,000", 10000);
      CreateAnOTBCard(
         19,
         "<color=orange>EQUIPMENT SALE</color>\nold but usable <b>TRACTOR</b>",
         "TRACTOR - Total $10,000", 10000);
      CreateAnOTBCard(
         20,
         "<color=orange>EQUIPMENT SALE</color>\nold but usable <b>HARVESTER</b>",
         "HARVESTER - Total $10,000", 10000);
      CreateAnOTBCard(
         21,
         "<color=orange>EQUIPMENT SALE</color>\nold but usable <b>HARVESTER</b>",
         "HARVESTER - Total $10,000", 10000);
      CreateAnOTBCard(
         22,
         "<color=orange>EQUIPMENT SALE</color>\nold but usable <b>HARVESTER</b>",
         "HARVESTER - Total 10,000", 10000);
      CreateAnOTBCard(
         23,
         "<color=red>NEIGHBORING FARMER GOES BROKE</color>\n<b>5 acres of Fruit</b> at 5000 per acre",
         "5 acres FRUIT - Total $25,000", 25000);
      CreateAnOTBCard(
         24,
         "<color=red>NEIGHBORING FARMER GOES BROKE</color>\n<b>5 acres of Fruit</b> at 5000 per acre",
         "5 acres FRUIT - Total $25,000", 25000);
      CreateAnOTBCard(
         25,
         "<color=red>NEIGHBORING FARMER GOES BROKE</color>\n<b>5 acres of Fruit</b> at 5000 per acre",
         "5 acres FRUIT - Total $25,000", 25000);
      CreateAnOTBCard(
         26,
         "<color=red>NEIGHBORING FARMER GOES BROKE</color>\n<b>5 acres of Fruit</b> at 5000 per acre",
         "5 acres FRUIT - Total $25,000", 25000);
      CreateAnOTBCard(
         27,
         "<color=red>NEIGHBORING FARMER GOES BROKE</color>\n<b>5 acres of Fruit</b> at 5000 per acre",
         "5 acres FRUIT - Total $25,000", 25000);
      CreateAnOTBCard(
         28,
         "<color=red>NEIGHBORING FARMER GOES BROKE</color>\n<b>5 acres of Fruit</b> at 5000 per acre",
         "5 acres FRUIT - Total $25,000", 25000);
      CreateAnOTBCard(
         29,
         "<color=#B27738>LEASE LEMHI RANGE</color>\nfor lifetime at 25,000\n<b>and buy 50 pregnant cows to stock it</b> at 500 each",
         "LEASE Lemhi Range - Total $50,000", 50000);
      CreateAnOTBCard(
         30,
         "<color=#B27738>LEASE LEMHI RANGE</color>\nfor lifetime at 25,000\n<b>and buy 50 pregnant cows to stock it</b> at 500 each",
         "LEASE Lemhi Range - Total $50,000", 50000);
      CreateAnOTBCard(
         31,
         "<color=#B27738>LEASE LEMHI RANGE</color>\nfor lifetime at 25,000\n<b>and buy 50 pregnant cows to stock it</b> at 500 each",
         "LEASE Lemhi Range - Total $50,000", 50000);
      CreateAnOTBCard(
         32,
         "<color=#B27738>LEASE LOST RIVER RANGE</color>\nfor lifetime at 20,000\n<b>and buy 40 pregnant cows to stock</b> it at 500 each",
         "LEASE Lost River Range - Total $40,000", 40000);
      CreateAnOTBCard(
         33,
         "<color=#B27738>LEASE LOST RIVER RANGE</color>\nfor lifetime at 20,000\n<b>and buy 40 pregnant cows to stock it</b> at 500 each",
         "LEASE Lost River Range - Total $40,000", 40000);
      CreateAnOTBCard(
         34,
         "<color=#B27738>LEASE LOST RIVER RANGE</color>\nfor lifetime at 20,000\n<b>and buy 40 pregnant cows to stock</b> it at 500 each",
         "LEASE Lost River Range - Total $40,000", 40000);
      CreateAnOTBCard(
         35,
         "<color=#B27738>LEASE TARGHEE RANGE</color>\nfor lifetime at 15,000\n<b>and buy 30 pregnant cows to stock it</b> at 500 each",
         "LEASE Targhee Range - Total $30,000", 30000);
      CreateAnOTBCard(
         36,
         "<color=#B27738>LEASE TARGHEE RANGE</color>\nfor lifetime at 15,000\n<b>and buy 30 pregnant cows to stock it</b> at 500 each",
         "LEASE Targhee Range - Total $30,000", 30000);
      CreateAnOTBCard(
         37,
         "<color=#B27738>LEASE TARGHEE RANGE</color>\nfor lifetime at 15,000\n<b>and buy 30 pregnant cows to stock it</b> at 500 each",
         "LEASE Targhee Range - Total $30,000", 30000);
      CreateAnOTBCard(
         38,
         "<color=#B27738>LEASE OXFORD RANGE</color>\nfor lifetime at 10,000\n<b>and buy 20 pregnant cows to stock it</b> at 500 each",
         "LEASE Oxford Range - Total $20,000", 20000);
      CreateAnOTBCard(
         39,
         "<color=#B27738>LEASE OXFORD RANGE</color>\nfor lifetime at 10,000\n<b>and buy 20 pregnant cows to stock it</b> at 500 each",
         "LEASE Oxford Range - Total $20,000", 20000);
      CreateAnOTBCard(
         40,
         "<color=#B27738>LEASE OXFORD RANGE</color>\nfor lifetime at 10,000\n<b>and buy 20 pregnant cows to stock it</b> at 500 each",
         "LEASE Oxford Range - Total $20,000", 20000);
      CreateAnOTBCard(
         41,
         "<color=#00A1D6FF>NEIGHBORING FARMER SELLS OUT</color>\n<b>10 acres of Spuds</b> at 2000 per acre",
         "10 acres SPUDS - Total $20,000", 20000);
      CreateAnOTBCard(
         42,
         "<color=#00A1D6FF>NEIGHBORING FARMER SELLS OUT</color>\n<b>10 acres of Spuds</b> at 2000 per acre",
         "10 acres SPUDS - Total $20,000", 20000);
      CreateAnOTBCard(
         43,
         "<color=#00A1D6FF>NEIGHBORING FARMER SELLS OUT</color>\n<b>10 acres of Spuds</b> at 2000 per acre",
         "10 acres SPUDS - Total $20,000", 20000);
      CreateAnOTBCard(
         44,
         "<color=#00A1D6FF>NEIGHBORING FARMER SELLS OUT</color>\n<b>10 acres of Spuds</b> at 2000 per acre",
         "10 acres SPUDS - Total $20,000", 20000);
      CreateAnOTBCard(
         45,
         "<color=#00A1D6FF>NEIGHBORING FARMER SELLS OUT</color>\n<b>10 acres of Spuds</b> at 2000 per acre",
         "10 acres SPUDS - Total $20,000", 20000);
      CreateAnOTBCard(
         46,
         "<color=#00A1D6FF>NEIGHBORING FARMER SELLS OUT</color>\n<b>10 acres of Spuds</b> at 2000 per acre",
         "10 acres SPUDS - Total $20,000", 20000);

      ShuffleOtbDeck(otbCards);

      //foreach (OTBCard oTBCard in otbCards)
      //	print(oTBCard.cardNumber + ":" + oTBCard.bottomCard + ":" + oTBCard.totalCost);
   }

   void CreateAnOTBCard(int cardNum, string desc, string sum, int cost)
   {
      //cardNum = 18;  //TRACTOR	TESTING
      //desc = "TRACTOR";
      //sum = "Tractor - 10000";
      ////cost = 10000;

      //cardNum = 21;  //HARVESTER	TESTING
      //desc = "HARVESTER";
      //sum = "Harvester - 10000";
      //cost = 10000;

      //cardNum = 39;  //OXFORD TESTING
      //desc = "OXFORD";
      //sum = "Oxford - 20000";
      //cost = 20000;

      //cardNum = 30;  //LEMHI TESTING
      //desc = "LEMHI";
      //sum = "Lemhi - 50000";
      //cost = 50000;

      //cardNum = 15;   //HAY TESTING
      //desc = "HAY";
      //sum = "HAY - 20000";
      //cost = 20000;

      //cardNum = 1;   //GRAIN TESTING
      //desc = "GRAIN";
      //sum = "GRAIN - 20000";
      //cost = 20000;

      //cardNum = 25;  //FRUIT TESTING
      //desc = "FRUIT";
      //sum = "FRUIT - 25000";
      //cost = 25000;

      //cardNum = 9;    //COW TESTING
      //desc = "COWS";
      //sum = "Cows - 5000";
      //cost = 5000;

      //cardNum = 44;   //SPUD TESTING
      //desc = "SPUDS";
      //sum = "Spuds - 20000";
      //cost = 20000;

      //declare an OTBCard object
      OTBCard newOTBCard = new OTBCard();

      //set the fields
      newOTBCard.cardNumber = cardNum;
      newOTBCard.description = desc;
      newOTBCard.summary = sum;
      newOTBCard.totalCost = cost;

      //add the new card to the list
      otbCards.Add(newOTBCard);
   }

   void ShuffleOtbDeck<T>(List<T> list)
   {
      Debug.Log($"DM.SOtbDeck: {otbCards.Count}");
      otbDeckShuffleCounter++;
      //Debug.Log($"IN SOTB's: {otbDeckShuffleCounter}");

      RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
      int n = list.Count;
      while (n > 1)
      {
         byte[] box = new byte[1];
         do provider.GetBytes(box);
         while (!(box[0] < n * (byte.MaxValue / n)));
         int k = (box[0] % n);
         n--;
         T value = list[k];
         list[k] = list[n];
         list[n] = value;
      }
      SetBottomOtbCard(otbCards);

      if (otbDeckShuffleCounter < 1) return;

      //TODO: CHANGE OVER TO MIRROR

      ////send msg to all to update their shuffle counter text
      ////event data - deck,counter
      //object[] sndData = new object[] { "OTB", otbDeckShuffleCounter };
      ////event options
      //RaiseEventOptions eventOptions = new RaiseEventOptions()
      //{
      //   Receivers = ReceiverGroup.All,
      //   CachingOption = EventCaching.DoNotCache
      //};
      ////send options
      //SendOptions sendOptions = new SendOptions() { Reliability = true };
      ////fire the event
      //PhotonNetwork.RaiseEvent((byte)RaiseEventCodes3D.SHUFFLE_DECK_EVENT, sndData, eventOptions, sendOptions);
   }

   void SetBottomOtbCard(List<OTBCard> deck)
   {
      for (int i = 0; i < deck.Count; i++)
      {
         if (i == deck.Count - 1)
            deck[i].bottomCard = true;
         else
            deck[i].bottomCard = false;
      }
      //Debug.Log($"OTB: {otbCards.Count}");
   }

   public void DrawOTBCard(GameObject target)
   {
      Debug.Log($"Called PM.DrawOTBCard: {target.name}");

      NetworkIdentity conn = target.GetComponent<NetworkIdentity>();

      OTBCard drawnCard = otbCards[0];
      otbCards.Remove(drawnCard);

      TargetGiveOTBCard(conn.connectionToClient, target, drawnCard);
   }

   [TargetRpc]
   public void TargetGiveOTBCard(NetworkConnectionToClient target, GameObject go, OTBCard card)
   {
      Debug.Log($"In TRPC: {card.cardNumber}");

      go.GetComponent<PlayerManager>().ReceiveOTBCard(card);
   }

   public void ReturnOTBCardToDeck(OTBCard card)
   {
      otbCards.Add(card);

      //Debug.Log($"In DM.ROtbCard: {otbCards.Count}");

      if (card.bottomCard)
         ShuffleOtbDeck(otbCards);

      UpdateDeckInfo("OTB");

      //Debug.Log($"DM: {card.cardNumber} was returned to deck");
   }

   void UpdateDeckInfo(string deck)
   {
      //TODO: UPDATE DECK INFO
   }
}
