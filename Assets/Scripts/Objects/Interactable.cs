﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Interactable : MonoBehaviour
{
    public Action<bool> hilightChangedDelegate; 
    public Action<Interactable, Hand, Tool.ToolType> interactDelegate;
    public Tool.ToolType neededTool;
    public bool isSmall = false;

    public void SetHilight(bool hilight)
    {
        if(hilightChangedDelegate != null)
            hilightChangedDelegate(hilight);
    }

    public void OnInteractionBy(Hand hand, Tool.ToolType toolType)
    {
        if(interactDelegate != null)
            interactDelegate(this, hand, toolType);
    }
}
