using System.Collections;
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
    public float initialWaitTime;
    public bool repairFinish = false;
    public bool shipIsArrived = false;

    [Header("Prefabs and settings")]
    public GameObject prefabSpaceship;
    public Transform shipSpawnPos;
    public Transform shipRepairPos;
    public GameObject entryDoor;
    public GameObject exitDoor;

    [Header("UI")]
    public TextMeshProUGUI timerUI;
    public TextMeshProUGUI brokenPartCount;
    public GameObject pilotAnim;
    public Animator animatorUI;

    private float waitTimer;
    private float repairTimer;
    private float leavingTimer;
    private GameObject currentShip;
    private Ship currentShipScript;

    private bool shipWarningPlayed = false;


    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();

        if(instance == null)
        {
            instance = this;
        }

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
                    if(waitTimer >= 2.5 && !shipWarningPlayed)
                    {
                        SoundManagerScript.instance.PlayOneShotSound(SoundManagerScript.AudioClips.ShipWarning);
                        shipWarningPlayed = true;
                    }
                } else {
                    shipWarningPlayed = false;
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
            animatorUI.SetTrigger("Happy");
            Repair();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            
        }

    }


    IEnumerator ArrivingShip()
    {

        repairTimer = 0.0f;

        //SoundManagerScript.instance.PlayOneShotSound(SoundManagerScript.AudioClips.ShipArriving);

        currentShip = Instantiate(prefabSpaceship, shipSpawnPos.position, Quaternion.Euler(0,90,0));
        currentShipScript = currentShip.GetComponent<Ship>();

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

    }

   

}
