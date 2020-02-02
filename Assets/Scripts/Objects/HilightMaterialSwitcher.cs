﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class HilightMaterialSwitcher: MonoBehaviour
{
    public Material basicMaterial;
    public Material hilightedMaterial;
    public new Renderer renderer;

    void Start()
    {
        GetComponent<Interactable>().hilightChangedDelegate += OnHilightChanged;
        if(renderer == null)
            renderer = GetComponent<Renderer>();
        
    }

    public void OnHilightChanged(bool hilighted)
    {
        renderer.sharedMaterial = hilighted ? hilightedMaterial : basicMaterial;

    }
}
