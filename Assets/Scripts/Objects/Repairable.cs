﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RepairState
{
    Repaired, Twisted, Unpainted, Damaged 
}
public class Repairable : MonoBehaviour
{
    public Tool.ToolType toolType;
    public Transform repairedPrefab;
    public Transform twistedPrefab;
    public Transform unpaintedPrefab;
    public Transform damagedPrefab;

    public RepairState currentState;
    private Transform spawnedVersion;

    public void Start()
    {
        SpawnVersion(currentState);

    }

    public void OnDestroy()
    {
        if(spawnedVersion != null)
        {
            GetComponent<Interactable>().hilightChangedDelegate -= spawnedVersion.GetComponentInChildren<HilightMaterialSwitcher>().OnHilightChanged;
        }

    }

    public void SpawnVersion(RepairState newState)
    {
        currentState = newState;
        if(spawnedVersion != null)
        {
            GetComponent<Interactable>().hilightChangedDelegate -= spawnedVersion.GetComponentInChildren<HilightMaterialSwitcher>().OnHilightChanged;
            Destroy(spawnedVersion.gameObject);
        }
        switch(newState)
        {
            case RepairState.Repaired:
                spawnedVersion = Instantiate(repairedPrefab, transform);
                break;
            case RepairState.Twisted:
                spawnedVersion = Instantiate(twistedPrefab, transform);
                break;
            case RepairState.Unpainted:
                spawnedVersion = Instantiate(unpaintedPrefab, transform);
                break;
            case RepairState.Damaged:
                spawnedVersion = Instantiate(damagedPrefab, transform);
                break;
        }
        GetComponent<Interactable>().hilightChangedDelegate += spawnedVersion.GetComponentInChildren<HilightMaterialSwitcher>().OnHilightChanged;

    }

    public void RepairWithTool(Tool.ToolType toolType)
    {
        switch(toolType)
        {
            case Tool.ToolType.Hammer:
                if(currentState == RepairState.Twisted)
                {
                    SpawnVersion(RepairState.Repaired);
                }
                break;
            case Tool.ToolType.BlowPipe:
                if(currentState == RepairState.Damaged)
                {
                    SpawnVersion(RepairState.Repaired);
                }
                break;
        }
    }
}
