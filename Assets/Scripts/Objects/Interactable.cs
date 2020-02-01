using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Interactable : MonoBehaviour
{
    public Action<bool> hilightChangedDelegate; 
    public Action<Hand> interactDelegate;

    public void SetHilight(bool hilight)
    {
        if(hilightChangedDelegate != null)
            hilightChangedDelegate(hilight);
    }

    public void OnInteractionBy(Hand hand)
    {
        if(interactDelegate != null)
            interactDelegate(hand);
    }
}
