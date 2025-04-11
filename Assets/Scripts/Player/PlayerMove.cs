using Cinemachine;
using DG.Tweening;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : NetworkBehaviour
{
   #region Fields & Properties

   [SerializeField] PlayerPositionsSO playerPositions;
   [SerializeField] float normalMoveSpeed, directedMoveSpeed, ludicrousSpeed;
   [SerializeField] Animator shifterAnim;
   [SerializeField] CinemachineVirtualCamera closeupCam;
   [SerializeField] DOTweenAnimation[] rearWheelTweeners;
   [SerializeField] DOTweenAnimation[] frontWheelTweeners;
   [SerializeField] float rearWheelDuration = 1f;
   [SerializeField] float frontWheelDuration = 0.5f;
   [SerializeField] PlayerManager pManager;
   [SerializeField] PlayerSetup pSetup;
   [SerializeField] GameObject tractor, dragster;
   [SerializeField] GameObject spotLight;
   [SerializeField] Tractor myTractor;

   //[SerializeField] float lerpSpeed = 10;
   public CinemachineVirtualCamera followCam;   //set in PlayerSetup

   CinemachineBrain brain;
   //FollowCam followCamSpotLight;
   //UIManager uiManager;
   //GameManager gm;
   Rigidbody rb;

   int die;
   [SerializeField] float moveSpeed;
   public int currentSpace;
   [SerializeField] int targetSpace;  //serialized for testing
   [SerializeField] Vector3 currentPosition, currentTarget, finalTarget;
   int startSpace, endSpace;
   //bool isDirectedMove, checkpointReached;
   int cowCounter, currentYear = 1;
   bool initialSpot = true;
   bool usingDragster;

   Queue<int> waypoints = new Queue<int>();
   Queue<int> route = new Queue<int>();

   #endregion

   #region Unity Callbacks

   void OnTriggerEnter(Collider other)
   {
      int thisSpace;
      bool sameSpace;

      if (other.CompareTag("Space Info"))
      {
         thisSpace = int.Parse(other.name);

         sameSpace = thisSpace == currentSpace;

         if (!sameSpace)
         {
            currentSpace = thisSpace;

            if (currentSpace == 0 && !initialSpot)
            {
               //EndOfYearResets();
            }
         }

         Debug.Log($"SPACE: {currentSpace}");
      }
   }
   #endregion

   #region Mirror Callbacks

   public override void OnStartAuthority()
   {
      //uiManager = UIManager.Instance;
      //gm = GameManager.Instance;
      rb = GetComponent<Rigidbody>();
      brain = Camera.main.GetComponent<CinemachineBrain>();
      pManager = myTractor.GetMyPlayerManager();
      pSetup = myTractor.GetMyPlayerSetup();

      UpdateMyYearToAll();
   }
   #endregion

   //called from the RollButton
   public void InitMove(int _die)
   {
      die = _die;

      Debug.Log($"InitMove: in-{_die}::out-{die}");
      //die = 37;	//TESTING

#if UNITY_EDITOR
      die = IFG.LoadedDie(die);
#endif

      moveSpeed = normalMoveSpeed;

      currentPosition = transform.position;

      //ToggleSpotLight(true);

      if (die == -9)
         StartCoroutine(ReverseRoutine());
      else
         StartCoroutine(MoveRoutine());
   }

   public void DirectedMove(int space) //use the end space number
   {
      if (space == 52)
      {
         moveSpeed = ludicrousSpeed;
         tractor.SetActive(false);
         dragster.SetActive(true);
         ToggleSpotLight(false);
         usingDragster = true;
      }
      else
         moveSpeed = directedMoveSpeed;

      die = space - currentSpace;
      StartCoroutine(MoveRoutine());
   }

   IEnumerator MoveRoutine()
   {
      startSpace = currentSpace;
      endSpace = currentSpace + die;

      //setup the move...

      //find finalTarget
      targetSpace = endSpace;
      GetMyTargetPosition();
      finalTarget = currentTarget;

      //Debug.Log($"Move Speed: {moveSpeed}");

      //uiManager.endTurnButton.interactable = false;

      currentTarget = Vector3.zero;
      targetSpace = 0;

      SetWaypoints();
      BuildTheRoute();
      GetMyTargetPosition();
      RunTheRoute();

      //pSetup.GetLocalPlayerIndicator().SetActive(false); ;
      //followCam.Priority = 15;
      //ToggleSpotLight(true);

      yield return null;

      if (die > 0)
      {
         foreach (var tweener in rearWheelTweeners)
         {
            tweener.duration *= (moveSpeed / normalMoveSpeed);
            tweener.DOPlayForward();
         }

         foreach (var tweener in frontWheelTweeners)
         {
            tweener.duration *= (moveSpeed / normalMoveSpeed);
            tweener.DOPlayForward();
         }
      }

      if (!usingDragster)
      {
         //AudioManager.Instance.Sources[20].pitch *= moveSpeed / normalMoveSpeed;
         //AudioManager.Instance.PlaySound(20);
      }
      else
         //AudioManager.Instance.PlaySound(5);

         RpcPlayerRemoteMoveSound();

      if (die >= 1 && die <= 3)
         brain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
      else
         brain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;

      Debug.Log($"In MR B4 while loop: {die}==:{finalTarget}");

      while (transform.position != finalTarget)
      {
         transform.position = Vector3.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime);

         currentPosition = transform.position;

         if (die > 0)
            transform.LookAt(currentTarget);

         if (transform.position == currentTarget)
            RunTheRoute();

         yield return null;
      }

      Debug.Log($"Out of loop: {transform.position}:{finalTarget}");

      initialSpot = false;
      finalTarget = Vector3.zero;
      currentTarget = Vector3.zero;
      route.Clear();
      dragster.SetActive(false);
      tractor.SetActive(true);
      //followCam.Priority = 5;
      //pSetup.GetLocalPlayerIndicator().SetActive(true); ;
      moveSpeed = normalMoveSpeed;
      //AudioManager.Instance.StopAllSources();
      //AudioManager.Instance.Sources[20].pitch = 1.28f;
      RpcStopRemotePlayerMoveSound();
      usingDragster = false;

      //ToggleSpotLight(false);

      foreach (var tweener in rearWheelTweeners)
      {
         tweener.duration = rearWheelDuration;
         tweener.DOPause();
      }

      foreach (var tweener in frontWheelTweeners)
      {
         tweener.duration = frontWheelDuration;
         tweener.DOPause();
      }

      EndOfMove();
   }

   [ClientRpc(includeOwner = false)]
   void RpcPlayerRemoteMoveSound()
   {
      //AudioManager.Instance.PlaySound(20, 0.5f);
   }

   [ClientRpc(includeOwner = false)]
   void RpcStopRemotePlayerMoveSound()
   {
      //AudioManager.Instance.StopAllSources();
   }

   void SetWaypoints()
   {
      waypoints.Clear();

      int totalMove = startSpace - endSpace;

      for (int i = startSpace; i <= endSpace; i++)
      {
         if (i == 14 && currentSpace != 14)
         {
            waypoints.Enqueue(i);
            Debug.Log($"Waypoint at: {i}");
         }
         if (i == 25 && currentSpace != 25)
         {
            waypoints.Enqueue(i);
            Debug.Log($"Waypoint at: {i}");
         }
         if (i == 36 && currentSpace != 36)
         {
            waypoints.Enqueue(i);
            Debug.Log($"Waypoint at: {i}");
         }
         if (i == 50 && currentSpace != 50)
         {
            waypoints.Enqueue(i);
            Debug.Log($"Waypoint at: {i}");
         }
      }
   }

   void BuildTheRoute()
   {
      if (waypoints.Count > 0)
      {
         for (int i = waypoints.Count - 1; i >= 0; i--)
         {
            int nextTarget = waypoints.Dequeue();
            route.Enqueue(nextTarget);

            Debug.Log($"In BuildRoute WP: {nextTarget}");
         }
      }
      //just straight move to endpoint
      route.Enqueue(endSpace);

      Debug.Log($"ROUTE WAYPOINTS: {route.Count}");
   }

   void RunTheRoute()
   {
      if (route.Count == 0) return;

      targetSpace = route.Dequeue();
      GetMyTargetPosition();
   }

   void GetMyTargetPosition()
   {
      if (targetSpace == 1)
      {
         switch (myTractor.GetFarmerName())
         {
            case IFG.RON:
               currentTarget = playerPositions.ronPos01;
               break;

            case IFG.BECKY:
               currentTarget = playerPositions.becPos01;
               break;

            case IFG.MIKE:
               currentTarget = playerPositions.mikPos01;
               break;

            case IFG.JANIS:
               currentTarget = playerPositions.janPos01;
               break;

            case IFG.RIC:
               currentTarget = playerPositions.ricPos01;
               break;

            case IFG.JERRY:
               currentTarget = playerPositions.jerPos01;
               break;

            default:
               Debug.LogWarning("Invalid Farmer: " + myTractor.GetFarmerName());
               break;
         }
      }

      //Going across the north board path.
      if (targetSpace >= 2 && targetSpace < 14)
      {
         CalcTargetPosBetweenCorners();
      }
      //Spring Planting.
      if (targetSpace == 14)
      {
         switch (myTractor.GetFarmerName())
         {
            case IFG.RON:
               currentTarget = playerPositions.ronPos14;
               break;

            case IFG.BECKY:
               currentTarget = playerPositions.becPos14;
               break;

            case IFG.MIKE:
               currentTarget = playerPositions.mikPos14;
               break;

            case IFG.JANIS:
               currentTarget = playerPositions.janPos14;
               break;

            case IFG.RIC:
               currentTarget = playerPositions.ricPos14;
               break;

            case IFG.JERRY:
               currentTarget = playerPositions.jerPos14;
               break;
         }
      }

      //1st space south of Spring Planting.
      if (targetSpace == 15)
      {
         switch (myTractor.GetFarmerName())
         {
            case IFG.RON:
               currentTarget = playerPositions.ronPos15;
               break;

            case IFG.BECKY:
               currentTarget = playerPositions.becPos15;
               break;

            case IFG.MIKE:
               currentTarget = playerPositions.mikPos15;
               break;

            case IFG.JANIS:
               currentTarget = playerPositions.janPos15;
               break;

            case IFG.RIC:
               currentTarget = playerPositions.ricPos15;
               break;

            case IFG.JERRY:
               currentTarget = playerPositions.jerPos15;
               break;
         }
      }
      //Going down the east board path.
      if (targetSpace >= 16 && targetSpace < 25)
      {
         CalcTargetPosBetweenCorners();
      }
      //Independence Day Bash.
      if (targetSpace == 25)
      {
         switch (myTractor.GetFarmerName())
         {
            case IFG.RON:
               currentTarget = playerPositions.ronPos25;
               break;

            case IFG.BECKY:
               currentTarget = playerPositions.becPos25;
               break;

            case IFG.MIKE:
               currentTarget = playerPositions.mikPos25;
               break;

            case IFG.JANIS:
               currentTarget = playerPositions.janPos25;
               break;

            case IFG.RIC:
               currentTarget = playerPositions.ricPos25;
               break;

            case IFG.JERRY:
               currentTarget = playerPositions.jerPos25;
               break;
         }
      }
      //1st space west of Independence Day Bash.
      if (targetSpace == 26)
      {
         switch (myTractor.GetFarmerName())
         {
            case IFG.RON:
               currentTarget = playerPositions.ronPos26;
               break;

            case IFG.BECKY:
               currentTarget = playerPositions.becPos26;
               break;

            case IFG.MIKE:
               currentTarget = playerPositions.mikPos26;
               break;

            case IFG.JANIS:
               currentTarget = playerPositions.janPos26;
               break;

            case IFG.RIC:
               currentTarget = playerPositions.ricPos26;
               break;

            case IFG.JERRY:
               currentTarget = playerPositions.jerPos26;
               break;
         }
      }
      //Going across the south board path.
      if (targetSpace >= 27 && targetSpace < 36)
      {
         CalcTargetPosBetweenCorners();
      }
      //Harvest Moon
      if (targetSpace == 36)
      {
         switch (myTractor.GetFarmerName())
         {
            case IFG.RON:
               currentTarget = playerPositions.ronPos36;
               break;

            case IFG.BECKY:
               currentTarget = playerPositions.becPos36;
               break;

            case IFG.MIKE:
               currentTarget = playerPositions.mikPos36;
               break;

            case IFG.JANIS:
               currentTarget = playerPositions.janPos36;
               break;

            case IFG.RIC:
               currentTarget = playerPositions.ricPos36;
               break;

            case IFG.JERRY:
               currentTarget = playerPositions.jerPos36;
               break;
         }
      }
      //1st space north of Harvest Moon.
      if (targetSpace == 37)
      {
         switch (myTractor.GetFarmerName())
         {
            case IFG.RON:
               currentTarget = playerPositions.ronPos37;
               break;

            case IFG.BECKY:
               currentTarget = playerPositions.becPos37;
               break;

            case IFG.MIKE:
               currentTarget = playerPositions.mikPos37;
               break;

            case IFG.JANIS:
               currentTarget = playerPositions.janPos37;
               break;

            case IFG.RIC:
               currentTarget = playerPositions.ricPos37;
               break;

            case IFG.JERRY:
               currentTarget = playerPositions.jerPos37;
               break;
         }
      }
      //Going up the west board path.
      if (targetSpace >= 38 && targetSpace < 50)
      {
         CalcTargetPosBetweenCorners();
      }

      if (targetSpace == 50)
      {
         targetSpace = 0;

         //Set the player to his Christmas Vacation spot.
         switch (myTractor.GetFarmerName())
         {
            case IFG.RON:
               currentTarget = playerPositions.ronPos00;
               break;

            case IFG.BECKY:
               currentTarget = playerPositions.becPos00;
               break;


            case IFG.MIKE:
               currentTarget = playerPositions.mikPos00;
               break;

            case IFG.JANIS:
               currentTarget = playerPositions.janPos00;
               break;

            case IFG.RIC:
               currentTarget = playerPositions.ricPos00;
               break;

            case IFG.JERRY:
               currentTarget = playerPositions.jerPos00;
               break;
         }
      }

      if (targetSpace > 50)
      {
         switch (myTractor.GetFarmerName())
         {
            case IFG.RON:
               currentTarget = playerPositions.ronPos01 + (endSpace - 51) * playerPositions.northSideDif;
               break;
            case IFG.JANIS:
               currentTarget = playerPositions.janPos01 + (endSpace - 51) * playerPositions.northSideDif;
               break;
            case IFG.JERRY:
               currentTarget = playerPositions.jerPos01 + (endSpace - 51) * playerPositions.northSideDif;
               break;
            case IFG.RIC:
               currentTarget = playerPositions.ricPos01 + (endSpace - 51) * playerPositions.northSideDif;
               break;
            case IFG.BECKY:
               currentTarget = playerPositions.becPos01 + (endSpace - 51) * playerPositions.northSideDif;
               break;
            case IFG.MIKE:
               currentTarget = playerPositions.mikPos01 + (endSpace - 51) * playerPositions.northSideDif;
               break;
         }
      }
   }

   void CalcTargetPosBetweenCorners()
   {
      if (endSpace > 1 && endSpace < 14)
      {
         switch (myTractor.GetFarmerName())
         {
            case IFG.RON:
               currentTarget = playerPositions.ronPos01 + (endSpace - 1) * playerPositions.northSideDif;
               break;
            case IFG.JANIS:
               currentTarget = playerPositions.janPos01 + (endSpace - 1) * playerPositions.northSideDif;
               break;
            case IFG.JERRY:
               currentTarget = playerPositions.jerPos01 + (endSpace - 1) * playerPositions.northSideDif;
               break;
            case IFG.RIC:
               currentTarget = playerPositions.ricPos01 + (endSpace - 1) * playerPositions.northSideDif;
               break;
            case IFG.BECKY:
               currentTarget = playerPositions.becPos01 + (endSpace - 1) * playerPositions.northSideDif;
               break;
            case IFG.MIKE:
               currentTarget = playerPositions.mikPos01 + (endSpace - 1) * playerPositions.northSideDif;
               break;
         }
      }

      if (endSpace > 15 && endSpace < 25)
      {
         switch (myTractor.GetFarmerName())
         {
            case IFG.RON:
               currentTarget = playerPositions.ronPos15 + (endSpace - 15) * playerPositions.eastSideDif;
               break;
            case IFG.JANIS:
               currentTarget = playerPositions.janPos15 + (endSpace - 15) * playerPositions.eastSideDif;
               break;
            case IFG.JERRY:
               currentTarget = playerPositions.jerPos15 + (endSpace - 15) * playerPositions.eastSideDif;
               break;
            case IFG.RIC:
               currentTarget = playerPositions.ricPos15 + (endSpace - 15) * playerPositions.eastSideDif;
               break;
            case IFG.BECKY:
               currentTarget = playerPositions.becPos15 + (endSpace - 15) * playerPositions.eastSideDif;
               break;
            case IFG.MIKE:
               currentTarget = playerPositions.mikPos15 + (endSpace - 15) * playerPositions.eastSideDif;
               break;
         }
      }

      if (endSpace > 26 && endSpace < 36)
      {
         switch (myTractor.GetFarmerName())
         {
            case IFG.RON:
               currentTarget = playerPositions.ronPos26 + (endSpace - 26) * playerPositions.southSideDif;
               break;
            case IFG.JANIS:
               currentTarget = playerPositions.janPos26 + (endSpace - 26) * playerPositions.southSideDif;
               break;
            case IFG.JERRY:
               currentTarget = playerPositions.jerPos26 + (endSpace - 26) * playerPositions.southSideDif;
               break;
            case IFG.RIC:
               currentTarget = playerPositions.ricPos26 + (endSpace - 26) * playerPositions.southSideDif;
               break;
            case IFG.BECKY:
               currentTarget = playerPositions.becPos26 + (endSpace - 26) * playerPositions.southSideDif;
               break;
            case IFG.MIKE:
               currentTarget = playerPositions.mikPos26 + (endSpace - 26) * playerPositions.southSideDif;
               break;
         }
      }

      if (endSpace > 37 && endSpace < 50)
      {
         switch (myTractor.GetFarmerName())
         {
            case IFG.RON:
               currentTarget = playerPositions.ronPos37 + (endSpace - 37) * playerPositions.westSideDif;
               break;
            case IFG.JANIS:
               currentTarget = playerPositions.janPos37 + (endSpace - 37) * playerPositions.westSideDif;
               break;
            case IFG.JERRY:
               currentTarget = playerPositions.jerPos37 + (endSpace - 37) * playerPositions.westSideDif;
               break;
            case IFG.RIC:
               currentTarget = playerPositions.ricPos37 + (endSpace - 37) * playerPositions.westSideDif;
               break;
            case IFG.BECKY:
               currentTarget = playerPositions.becPos37 + (endSpace - 37) * playerPositions.westSideDif;
               break;
            case IFG.MIKE:
               currentTarget = playerPositions.mikPos37 + (endSpace - 37) * playerPositions.westSideDif;
               break;
         }
      }

      if (endSpace >= 50)
      {
         if (currentSpace == 50)
            currentSpace = 0;

         switch (myTractor.GetFarmerName())
         {
            case IFG.RON:
               currentTarget = playerPositions.ronPos00 + (endSpace - 50) * playerPositions.northSideDif;
               break;
            case IFG.JANIS:
               currentTarget = playerPositions.janPos00 + (endSpace - 50) * playerPositions.northSideDif;
               break;
            case IFG.JERRY:
               currentTarget = playerPositions.jerPos00 + (endSpace - 50) * playerPositions.northSideDif;
               break;
            case IFG.RIC:
               currentTarget = playerPositions.ricPos00 + (endSpace - 50) * playerPositions.northSideDif;
               break;
            case IFG.BECKY:
               currentTarget = playerPositions.becPos00 + (endSpace - 50) * playerPositions.northSideDif;
               break;
            case IFG.MIKE:
               currentTarget = playerPositions.mikPos00 + (endSpace - 50) * playerPositions.northSideDif;
               break;
         }
      }
   }

   IEnumerator ReverseRoutine()
   {
      startSpace = currentSpace;
      endSpace = currentSpace + die;

      //setup the move...
      //find finalTarget
      targetSpace = endSpace;
      GetMyTargetPosition();
      finalTarget = currentTarget;

      brain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
      //pSetup.GetLocalPlayerIndicator().SetActive(false); ;
      closeupCam.Priority = 20;
      shifterAnim.SetBool("Reverse", true);

      //yield return null;

      //AudioManager.Instance.PlaySound(20);

      yield return new WaitForSeconds(2f);   //WAIT FOR SHIFTER ANIMATION

      //uiManager.endTurnButton.interactable = false;
      //AudioManager.Instance.PlaySound(21);

      RpcPlayerRemoteMoveSound();

      currentTarget = Vector3.zero;
      targetSpace = 0;

      SetWaypoints();
      BuildTheRoute();
      GetMyTargetPosition();
      RunTheRoute();

      closeupCam.Priority = 1;
      followCam.Priority = 15;
      //pSetup.GetLocalPlayerIndicator().SetActive(false); ;
      //spotLight.GetComponent<Light>().intensity = 2.5f;

      foreach (var tweener in rearWheelTweeners)
      {
         tweener.DOPlayBackwards();
      }

      foreach (var tweener in frontWheelTweeners)
      {
         tweener.DOPlayBackwards();
      }

      yield return null;

      while (transform.position != finalTarget)
      {
         transform.position = Vector3.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime);

         currentPosition = transform.position;

         if (die > 0)
            transform.LookAt(currentTarget);

         if (transform.position == currentTarget)
            RunTheRoute();

         yield return null;
      }

      //Debug.Log($"Out of loop: {transform.position}:{finalTarget}");

      shifterAnim.SetBool("Reverse", false);
      followCam.Priority = 5;
      closeupCam.Priority = 15;
      shifterAnim.SetBool("Forward", true);

      foreach (var tweener in rearWheelTweeners)
      {
         tweener.duration = rearWheelDuration;
         tweener.DOPause();
      }

      foreach (var tweener in frontWheelTweeners)
      {
         tweener.duration = frontWheelDuration;
         tweener.DOPause();
      }

      yield return new WaitForSeconds(2f);

      shifterAnim.SetBool("Forward", false);

      closeupCam.Priority = 1;
      finalTarget = Vector3.zero;
      currentTarget = Vector3.zero;
      route.Clear();
      moveSpeed = normalMoveSpeed;
      //AudioManager.Instance.StopAllSources();
      RpcStopRemotePlayerMoveSound();
      //spotLight.GetComponent<Light>().intensity = 4f;
      ToggleSpotLight(false);
      //pSetup.GetLocalPlayerIndicator().SetActive(true); ;

      EndOfMove();
   }

   //void EndOfYearResets()
   //{
   //   //Debug.Log("IN EOYR");
   //   currentYear++;
   //   UpdateMyYearToAll();

   //   //get $
   //   if (!gm.pManager.noWages)
   //      pManager.UpdateMyCash(5000);
   //   //deal with cows increased
   //   if (gm.pManager.cowsI)
   //   {
   //      cowCounter--;
   //      cowCounter = Mathf.Max(0, cowCounter);

   //      if (cowCounter == 0)
   //      {
   //         pManager.cowsI = false;
   //         pManager.HandleCommodityDoubled(IFG.Cows, false);
   //         ResetOwnedRanges();
   //         ResetCowCounter();
   //      }
   //   }
   //   //hay grain & spuds doubled
   //   pManager.HandleCommodityDoubled(IFG.Hay, false);
   //   pManager.HandleCommodityDoubled(IFG.Grain, false);
   //   if (pManager.spuds > 0)
   //      pManager.HandleCommodityDoubled(IFG.Spuds, false);

   //   ResetHarvests();
   //   ResetBadNews();
   //}

   //void ResetHarvests()
   //{
   //   pManager.firstHayH = false;
   //   pManager.secondHayH = false;
   //   pManager.thirdHayH = false;
   //   pManager.fourthHayH = false;
   //   pManager.cherryH = false;
   //   pManager.wheatH = false;
   //   pManager.livestockH = false;
   //   pManager.spudH = false;
   //   pManager.appleH = false;
   //   pManager.cornH = false;
   //   HarvestManager.Instance.SetCutNextHay(false);
   //}

   //void ResetBadNews()
   //{
   //   pManager.noWages = false;
   //   pManager.cherriesCutInHalf = false;
   //   pManager.wheatCutInHalf = false;
   //   pManager.UpdateMyGarnishedStatus(false);

   //   for (int i = 0; i < 4; i++)
   //   {
   //      uiManager.SetBadNewsElements(i, false);
   //   }
   //}

   //void ResetOwnedRanges()
   //{
   //   if (pManager.oxfordOwned)
   //      gm.pManager.HandleCommodityDoubled(IFG.Oxford, false);
   //   if (pManager.targheeOwned)
   //      gm.pManager.HandleCommodityDoubled(IFG.Targhee, false);
   //   if (pManager.lostRiverOwned)
   //      gm.pManager.HandleCommodityDoubled(IFG.LostRiver, false);
   //   if (pManager.lemhiOwned)
   //      gm.pManager.HandleCommodityDoubled(IFG.Lemhi, false);
   //}

   void EndOfMove()
   {
      //BoardManager.Instance.ShowSpace(currentSpace);
      //uiManager.endTurnButton.interactable = true;
   }

   public void ResetCowCounter()
   {
      cowCounter = 2;
   }

   void UpdateMyYearToAll()
   {
      //object[] sndData = new object[] { currentYear, gm.myFarmerName };

      //RaiseEventOptions eventOptions = new RaiseEventOptions()
      //{
      //   Receivers = ReceiverGroup.All,
      //   CachingOption = EventCaching.DoNotCache
      //};

      //PhotonNetwork.RaiseEvent((byte)RaiseEventCodes.UPDATE_YEAR_EVENT, sndData, eventOptions, SendOptions.SendReliable);
   }

   void ToggleSpotLight(bool state)
   {
      spotLight.SetActive(state);
   }

}
