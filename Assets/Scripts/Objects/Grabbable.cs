using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Interactable))]
public class Grabbable : MonoBehaviour
{
    private Interactable interactable;
    public Action<Hand> grabbedDelegate;
    public Action<Hand> releasedDelegate;

    void Start()
    {
        interactable = GetComponent<Interactable>();    
        interactable.interactDelegate += OnInteraction;
    }

    private void OnInteraction(Hand hand)
    {
        hand.GrabObject(this);
    }

    public void OnGrabbed(Hand hand)
    {
        if(grabbedDelegate != null)
        {
            grabbedDelegate(hand);
        }
    }

    public void OnReleased(Hand hand)
    {
        if(releasedDelegate != null)
        {
            releasedDelegate(hand);
        }
    }
}
