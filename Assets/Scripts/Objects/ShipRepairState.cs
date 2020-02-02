using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRepairState : MonoBehaviour
{
    public Repairable[] toRepair;   
    private Ship ship;
    void Start()
    {
        ship = GetComponent<Ship>();   
    }

    void Update()
    {
        int brokenPartCount = 0;
        foreach(Repairable repairable in toRepair)
        {
            if(!repairable.isActiveAndEnabled || (repairable.currentState != RepairState.Repaired && repairable.currentState != RepairState.Unpainted))
            {
                brokenPartCount++;
            }
        }
        ship.brokenPart = brokenPartCount;
        GameManagerScript.instance.UpdateText();
        if(brokenPartCount == 0)
        {
            GameManagerScript.instance.FinishedRepair();
            Destroy(this);

        }
        //TODO : Start Ship
    }
}
