using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Interactable))]
public class Grabbable : MonoBehaviour
{
    private Interactable interactable;
    public Action<Hand> grabbedDelegate;
    public Action startReleaseDelegate;
    public Action releasedDelegate;

    public delegate bool CanReleaseDelegate();
    public CanReleaseDelegate canReleaseDelegate;
    private bool releaseStarted = false;

    void Start()
    {
        interactable = GetComponent<Interactable>();    
        interactable.interactDelegate += OnInteraction;
    }

    private void OnInteraction(Hand hand)
    {
        hand.GrabObject(this);
    }

    private void Update()
    {
        if(releaseStarted)
        {
            if(canReleaseDelegate != null)
            {
                foreach(CanReleaseDelegate conditionDelegate in canReleaseDelegate.GetInvocationList())
                {
                    if(!conditionDelegate())
                    {
                        if(startReleaseDelegate != null)
                            startReleaseDelegate();
                        return;
                    }
                }
            }
            releaseStarted = false;
            if(releasedDelegate != null)
                releasedDelegate();
        }
    }

    public void OnGrabbed(Hand hand)
    {
        if(grabbedDelegate != null)
        {
            grabbedDelegate(hand);
        }
        releaseStarted = false;
    }

    public void OnReleased(Hand hand)
    {
        releaseStarted = true;
        if(startReleaseDelegate != null)
        {
            startReleaseDelegate();
        }
    }
}
