﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;


public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance = null;
   

    public enum GameState
    {
        Waiting,
        Arriving,
        Leaving,
        PitStop
    }

    public GameState currentState;

    [Header("Tweakable values")]
    public int totalStop;
    int currentStopCount;
    public float initialWaitTime;
    public bool repairFinish = false;
    public bool shipIsArrived = false;

    [Header("Prefabs and settings")]
    public GameObject prefabSpaceship;
    public Transform shipSpawnPos;
    public Transform shipRepairPos;
    public GameObject entryDoor;
    public GameObject exitDoor;
    public List<float> lapsTime;
    public List<TextMeshProUGUI> lapsUI;

    [Header("UI")]
    public TextMeshProUGUI timerUI;
    public TextMeshProUGUI brokenPartCount;
    public GameObject pilotAnim;
    public Animator animatorUI;

    private float waitTimer;
    private float leavingTimer;
    private float repairTimer;
    private float totalRunTime;
    private GameObject currentShip;
    private Ship currentShipScript;

    private bool shipWarningPlayed = false;
    private float minDelayBetweenDriveBy = 1.0f;
    private float maxDelayBetweenDriveBy = 5.0f;
    private float driveByTimer;


    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();

        if(instance == null)
        {
            instance = this;
        }
        lapsTime = new List<float>(3);

        waitTimer = 0.0f;
        currentState = GameState.Waiting;
        timerUI.text = "PREPARE THE PIT !";
        driveByTimer = Random.Range(minDelayBetweenDriveBy, maxDelayBetweenDriveBy);
    }

    // Update is called once per frame
    void Update()
    {
        driveByTimer -= Time.deltaTime;
        if(driveByTimer <= 0.0f)
        {
            driveByTimer = Random.Range(minDelayBetweenDriveBy, maxDelayBetweenDriveBy);
            //SoundManagerScript.instance.PlayOneShotSound(SoundManagerScript.AudioClips.ShipDriveBy);
        }

        switch (currentState)
        {
            case GameState.Waiting:
                if (waitTimer < 5) {
                    waitTimer += Time.deltaTime;
                    if(waitTimer >= 2.5 && !shipWarningPlayed) {
                        SoundManagerScript.instance.PlayOneShotSound(SoundManagerScript.AudioClips.ShipWarning);
                        shipWarningPlayed = true;
                    }
                } else {
                    if (currentStopCount < totalStop) {
                        shipWarningPlayed = false;
                        StartCoroutine(ArrivingShip());
                        timerUI.text = "BE PREPARED IT'S COMING !";
                        currentState = GameState.Arriving;
                    } else {
                        timerUI.text = "END OF THE RACE ! <br>TOTAL TIME :" + totalRunTime.ToString("f3");

                    }

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
                    string fmt = "00.###";
                    timerUI.text = repairTimer.ToString(fmt);
                }
                else
                {
                    lapsTime[currentStopCount] = repairTimer;
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
                    
                    currentStopCount++;
                    ResetSettings();
                    currentState = GameState.Waiting;
                }
                break;
            default:
                break;
        }

   
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(animatorUI != null)
                animatorUI.SetTrigger("Happy");
            Repair();
        }

    }


    IEnumerator ArrivingShip()
    {

        repairTimer = 0.0f;

        SoundManagerScript.instance.PlayOneShotSound(SoundManagerScript.AudioClips.ShipArriving);

        currentShip = Instantiate(prefabSpaceship, shipSpawnPos.position, Quaternion.Euler(0,90,0));
        currentShipScript = currentShip.GetComponent<Ship>();

        if(brokenPartCount != null && currentShipScript != null)
            brokenPartCount.text = currentShipScript.brokenPart.ToString();

        var initialYDoorPos = entryDoor.transform.position.y;

        SoundManagerScript.instance.PlayOneShotSound(SoundManagerScript.AudioClips.DoorsOpen);

        var doorTween = entryDoor.transform.DOMoveY(15, 1);
        yield return doorTween.WaitForCompletion();

        Sequence shipArrivingSeq = DOTween.Sequence();
        shipArrivingSeq.Append(currentShip.transform.DOMoveX(0, 1))
        .Append(currentShip.transform.DOMoveY(2, 1));
        yield return shipArrivingSeq.WaitForCompletion();

        shipIsArrived = true;
        if(animatorUI != null)
            animatorUI.SetTrigger("Angry");
        //StartCoroutine(pilotAnimation());

        SoundManagerScript.instance.PlayOneShotSound(SoundManagerScript.AudioClips.DoorsClose);

        //polish anim
        entryDoor.transform.DOMoveY(initialYDoorPos, 1);

    }

    IEnumerator LeavingShip()
    {
        SoundManagerScript.instance.PlayOneShotSound(SoundManagerScript.AudioClips.DoorsOpen);

        //freeze le timer
        var initialYDoorPos = entryDoor.transform.position.y;

        var doorTween = exitDoor.transform.DOMoveY(15, 1);
        yield return doorTween.WaitForCompletion();

        SoundManagerScript.instance.PlayOneShotSound(SoundManagerScript.AudioClips.ShipLeaving);

        Sequence shipLeavingSeq = DOTween.Sequence();
        shipLeavingSeq.Append(currentShip.transform.DOMoveY(4, 1))
            .Append(currentShip.transform.DOMoveX(50, 0.3f));
        yield return shipLeavingSeq.WaitForCompletion();

        SoundManagerScript.instance.PlayOneShotSound(SoundManagerScript.AudioClips.DoorsClose);
        exitDoor.transform.DOMoveY(initialYDoorPos, 1);

        Destroy(currentShip);
    }


    void Repair()
    {
        currentShipScript.brokenPart--;

        if (currentShipScript != null && currentShipScript.brokenPart <= 0 )
        {
            repairFinish = true;
        }

    }

    void ResetSettings()
    {
        //random le nombre
        currentShipScript.brokenPart = 5;
        repairFinish = false;
        shipIsArrived = false;
        waitTimer = 0f;

        totalRunTime += repairTimer;
        driveByTimer = Random.Range(minDelayBetweenDriveBy, maxDelayBetweenDriveBy);
    }

   

}
