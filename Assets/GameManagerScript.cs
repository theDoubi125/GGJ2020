using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;

public class GameManagerScript : MonoBehaviour
{
    [Header("Tweakable values")]
    public float initialWaitTime;


    [Header("Prefabs")]
    public GameObject prefabSpaceship;


    private GameObject currentShip;

    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator InitGame()
    {
        currentShip = Instantiate(prefabSpaceship);

        //TEMPO TIME (  10 SEC BEFORE THE FIRST SHIP ARRIVE )
        yield return new WaitForSeconds(initialWaitTime);

        //MAKE IT MOVE TO THE SPAWN POSITION
        currentShip.transform.

        


    }



    
}
