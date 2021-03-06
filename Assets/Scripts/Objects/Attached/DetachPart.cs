﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachPart : MonoBehaviour
{
    private Interactable interactable;
    public Transform toSpawn;
    public GameObject socket;

    void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.interactDelegate += OnInteraction;
    }

    private void OnInteraction(Interactable interactable, Hand hand, Tool.ToolType toolType)
    {


        if(toolType == Tool.ToolType.Wrench)
        {
            gameObject.SetActive(false);
            Transform spawned = Instantiate(toSpawn, transform.position, transform.rotation);
            spawned.localScale = transform.lossyScale;
            spawned.GetComponent<Repairable>().currentState = GetComponent<Repairable>().currentState;
            gameObject.SetActive(false);
            socket.SetActive(true);
        }
    }
    
    void Update()
    {
        
    }
}
