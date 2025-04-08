using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayer : NetworkBehaviour
{
   #region Fields & Properties

   [SyncVar]
   [SerializeField] string displayName = "Not Set...";
   [SyncVar]
   [SerializeField] string farmerName = "Not Set";

   //game info
   [SerializeField] int nop;
   [SerializeField] IFG.GameType gameType;
   [SerializeField] int networthAmount;
   [SerializeField] float timedLength;
   [SerializeField] bool borgDie;

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

   #region Setters & Getters

   public void SetNOP(int _nop)
   {
      nop = _nop;
   }

   public void SetGameType(IFG.GameType _gameType)
   {
      gameType = _gameType;
   }

   public void SetNetworthAmount(int _networthAmount)
   {
      networthAmount = _networthAmount;
   }

   public void SetTimedLength(float _timedLength)
   {
      timedLength = _timedLength;
   }

   public void SetBorgDie(bool _borgDie)
   {
      borgDie = _borgDie;
   }

   public int GetNOP()
   {
      return nop;
   }

   public IFG.GameType GetGameType()
   {
      return gameType;
   }

   public int GetNetworthAmount()
   {
      return networthAmount;
   }

   public float GetTimedLength()
   {
      return timedLength;
   }

   public bool GetBorgDie()
   {
      return borgDie;
   }

   #endregion

   #region Mirror Callbacks

   public override void OnStartClient()
   {
      DontDestroyOnLoad(gameObject);
      Room.GamePlayers.Add(this);
   }

   public override void OnStopClient()
   {
      Room.GamePlayers.Remove(this);
   }
   #endregion

   #region Client


   #endregion

   #region Server

   [Server]
   public void SetDisplayName(string newName)
   {
      displayName = newName;
   }

   [Server]
   public void SetFarmerName(string newName)
   {
      farmerName = newName;
   }

   #endregion

   #region Client

   public string GetFarmerName()
   {
      return farmerName;
   }
   #endregion
}

