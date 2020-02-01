using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;


public class GameManagerScript : MonoBehaviour
{
    public enum GameState
    {
        Waiting,
        Arriving,
        Leaving,
        PitStop
    }

    public GameState currentState;

    [Header("Tweakable values")]
    public float initialWaitTime;
    public bool repairFinish = false;
    public bool shipIsArrived = false;

    [Header("Prefabs and settings")]
    public GameObject prefabSpaceship;
    public Transform shipSpawnPos;
    public Transform shipRepairPos;

    [Header("Events")]
    public UnityEvent leavingEvent;

    [Header("UI")]
    public TextMeshProUGUI timerUI;

    private float waitTimer;
    private float repairTimer;
    private float leavingTimer;
    private GameObject currentShip;
    private Ship currentShipStat;
    

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();

        waitTimer = 0.0f;
        currentState = GameState.Waiting;
        timerUI.text = "PREPARE THE PIT !";
  
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case GameState.Waiting:
                if (waitTimer < 5) {
                    waitTimer += Time.deltaTime;
                } else {
                    StartCoroutine(ArrivingShip());
                    timerUI.text = "BE PREPARED IT'S COMING !";
                    currentState = GameState.Arriving;
                }
                break;
            case GameState.Arriving:
                if(shipIsArrived)
                {
                    currentState = GameState.PitStop;
                }
                break;
            case GameState.PitStop:
                if(!repairFinish)
                {
                    repairTimer += Time.deltaTime;
                    timerUI.text = repairTimer.ToString();
                }
                else
                {
                    StartCoroutine(LeavingShip());
                    leavingTimer = 0f;
                    currentState = GameState.Leaving;
                }
                break;
            case GameState.Leaving:
                if(leavingTimer < 3)
                {
                    leavingTimer += Time.deltaTime;
                }
                else
                {
                    ResetSettings();
                    currentState = GameState.Waiting;
                }
                break;
            default:
                break;
        }

   
        if (Input.GetKeyDown(KeyCode.R))
        {
            Repair();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            
        }

    }

    IEnumerator InitGame()
    {

        //TEMPO TIME (  10 SEC BEFORE THE FIRST SHIP ARRIVE )
        // yield return new WaitForSeconds(initialWaitTime);

        //MAKE IT MOVE TO THE SPAWN POSITION
        // ArrivingShip();

        yield return new WaitForSeconds(5);
        //LeavingShip();
    }


    IEnumerator ArrivingShip()
    {

        repairTimer = 0.0f;

        currentShip = Instantiate(prefabSpaceship, shipSpawnPos.position, Quaternion.identity);
        currentShipStat = currentShip.GetComponent<Ship>();

        Sequence shipArrivingSeq = DOTween.Sequence();
        shipArrivingSeq.Append(currentShip.transform.DOMoveX(0, 1))
        .Append(currentShip.transform.DOMoveY(1, 1));
        yield return shipArrivingSeq.WaitForCompletion();
        shipIsArrived = true;
    }

    IEnumerator LeavingShip()
    {
        //freeze le timer

        Sequence shipLeavingSeq = DOTween.Sequence();
        shipLeavingSeq.Append(currentShip.transform.DOMoveY(4, 1))
            .Append(currentShip.transform.DOMoveX(50, 0.3f));
        yield return shipLeavingSeq.WaitForCompletion();


    }


    void Repair()
    {
        currentShipStat.brokenPart--;

        if (currentShipStat != null && currentShipStat.brokenPart == 0 )
        {
            repairFinish = true;
        }

    }

    void ResetSettings()
    {
        //random le nombre
        currentShipStat.brokenPart = 5;

        waitTimer = 0f;

    }

}
