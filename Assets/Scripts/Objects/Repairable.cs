﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RepairState
{
    Repaired, Twisted, Unpainted, Damaged 
}
public class Repairable : MonoBehaviour
{
    public Transform repairedPrefab;
    public Transform twistedPrefab;
    public Transform unpaintedPrefab;
    public Transform damagedPrefab;

    public RepairState currentState;
    private Transform spawnedVersion;
    public Transform repairFXPrefab;

    public void Start()
    {
        SpawnVersion(currentState);
    }

    public void OnDestroy()
    {
        if(spawnedVersion != null)
        {
            HilightMaterialSwitcher hmatSwitcher = spawnedVersion.GetComponentInChildren<HilightMaterialSwitcher>();
            HilightAllMaterialsSwitcher hmatAllSwitcher = spawnedVersion.GetComponentInChildren<HilightAllMaterialsSwitcher>();
            if(hmatSwitcher != null)
                GetComponent<Interactable>().hilightChangedDelegate += hmatSwitcher.OnHilightChanged;
            if(hmatAllSwitcher != null)
                GetComponent<Interactable>().hilightChangedDelegate += hmatAllSwitcher.OnHilightChanged;
        }

    }

    public void SpawnVersion(RepairState newState)
    {
        currentState = newState;
        if(spawnedVersion != null)
        {
            HilightMaterialSwitcher hmatSwitcher = spawnedVersion.GetComponentInChildren<HilightMaterialSwitcher>();
            HilightAllMaterialsSwitcher hmatAllSwitcher = spawnedVersion.GetComponentInChildren<HilightAllMaterialsSwitcher>();
            if(hmatSwitcher != null)
                GetComponent<Interactable>().hilightChangedDelegate -= hmatSwitcher.OnHilightChanged;
            if(hmatAllSwitcher != null)
                GetComponent<Interactable>().hilightChangedDelegate -= hmatAllSwitcher.OnHilightChanged;
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
        {

            HilightMaterialSwitcher hmatSwitcher = spawnedVersion.GetComponentInChildren<HilightMaterialSwitcher>();
            HilightAllMaterialsSwitcher hmatAllSwitcher = spawnedVersion.GetComponentInChildren<HilightAllMaterialsSwitcher>();
            if(hmatSwitcher != null)
                GetComponent<Interactable>().hilightChangedDelegate += hmatSwitcher.OnHilightChanged;
            if(hmatAllSwitcher != null)
                GetComponent<Interactable>().hilightChangedDelegate += hmatAllSwitcher.OnHilightChanged;
        }

    }

    public void RepairWithTool(Tool.ToolType toolType)
    {
        switch(toolType)
        {
            case Tool.ToolType.Hammer:
                if(currentState == RepairState.Twisted)
                {
                    SpawnVersion(RepairState.Unpainted);
                    Instantiate(repairFXPrefab, transform.position, Quaternion.identity);
                    SoundManagerScript.instance.PlayOneShotSound(SoundManagerScript.AudioClips.ObjectRepair);
                }
                break;
            case Tool.ToolType.BlowPipe:
                if(currentState == RepairState.Damaged)
                {
                    SpawnVersion(RepairState.Unpainted);
                    Instantiate(repairFXPrefab, transform.position, Quaternion.identity);
                    SoundManagerScript.instance.PlayOneShotSound(SoundManagerScript.AudioClips.ObjectRepair);
                }
                break;
        }
    }

    public void Repaint()
    {
        
        if(currentState == RepairState.Unpainted)
        {
            Instantiate(repairFXPrefab, transform.position, Quaternion.identity);
            SpawnVersion(RepairState.Repaired);
            SoundManagerScript.instance.PlayOneShotSound(SoundManagerScript.AudioClips.ObjectPutColor);
        }
    }
}
