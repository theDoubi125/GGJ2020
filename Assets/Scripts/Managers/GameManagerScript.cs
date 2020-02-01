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

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        StartCoroutine(InitGame());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator InitGame()
    {

        //TEMPO TIME (  10 SEC BEFORE THE FIRST SHIP ARRIVE )
        yield return new WaitForSeconds(initialWaitTime);

        //MAKE IT MOVE TO THE SPAWN POSITION
        ArrivingShip();

        yield return new WaitForSeconds(5);
        LeavingShip();
    }


    void ArrivingShip()
    {
        currentShip = Instantiate(prefabSpaceship, shipSpawnPos.position, Quaternion.identity);

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

    
}
