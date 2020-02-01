using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class GameManagerScript : MonoBehaviour
{
    [Header("Tweakable values")]
    public float initialWaitTime;


    [Header("Prefabs and settings")]
    public GameObject prefabSpaceship;
    public Transform shipSpawnPos;
    public Transform shipRepairPos;


    private GameObject currentShip;
    private Ship currentShipStat;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        StartCoroutine(InitGame());

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Repair();
        }
    }

    IEnumerator InitGame()
    {

        //TEMPO TIME (  10 SEC BEFORE THE FIRST SHIP ARRIVE )
        yield return new WaitForSeconds(initialWaitTime);

        //MAKE IT MOVE TO THE SPAWN POSITION
        ArrivingShip();

        yield return new WaitForSeconds(5);
        //LeavingShip();
    }


    void ArrivingShip()
    {
        currentShip = Instantiate(prefabSpaceship, shipSpawnPos.position, Quaternion.identity);
        currentShipStat = currentShip.GetComponent<Ship>();

        Sequence shipArrivingSeq = DOTween.Sequence();
        shipArrivingSeq.Append(currentShip.transform.DOMoveX(0, 1))
        .Append(currentShip.transform.DOMoveY(1, 1));
    }

    void LeavingShip()
    {
        Sequence shipArrivingSeq = DOTween.Sequence();
        shipArrivingSeq.Append(currentShip.transform.DOMoveY(4, 1))
            .Append(currentShip.transform.DOMoveX(25, 0.3f));
    }

    
    void Repair()
    {
        currentShipStat.brokenPart--;

        if (currentShipStat != null && currentShipStat.brokenPart == 0 )
        {
            LeavingShip();
        }

    }

}
