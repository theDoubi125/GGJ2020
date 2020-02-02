using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRepairState : MonoBehaviour
{
    public Repairable[] toRepair;   
    void Start()
    {
        
    }

    void Update()
    {
        foreach(Repairable repairable in toRepair)
        {
            if(!repairable.isActiveAndEnabled || (repairable.currentState != RepairState.Repaired && repairable.currentState != RepairState.Unpainted))
            {
                return;
            }
        }
        //TODO : Start Ship
    }
}
