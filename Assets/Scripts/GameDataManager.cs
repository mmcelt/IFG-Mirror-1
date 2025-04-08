using Mirror;
using UnityEngine;

public class GameDataManager : NetworkBehaviour
{
   #region Fields & Properties

   public static GameDataManager Instance { get; private set; }

   [SyncVar]
   public int nop;
   [SyncVar]
   public IFG.GameType gameType;
   [SyncVar]
   public int nwAmount;
   [SyncVar]
   public float tgLength;
   [SyncVar]
   public bool useBorgDie;

   #endregion

   #region Unity Callbacks

   void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
         DontDestroyOnLoad(gameObject);
      }
      //else
      //   Destroy(gameObject);
   }

   //void Start() 
   //{
   //	
   //}

   //void Update()
   //{
   //   
   //}

   #endregion

   [Server]
   public void SetData(int _nop, IFG.GameType _gameType, int _nwAmount, float _tgLength, bool borgDie)
   {
      nop = _nop;
      gameType = _gameType;
      nwAmount = _nwAmount;
      tgLength = _tgLength;
      useBorgDie = borgDie;
   }
}

