using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class HilightMaterialSwitcher: MonoBehaviour
{
    public Material basicMaterial;
    public Material hilightedMaterial;
    private Renderer renderer;

    void Start()
    {
        GetComponent<Interactable>().hilightChangedDelegate += OnHilightChanged;
        renderer = GetComponent<Renderer>();
        
    }

    private void OnHilightChanged(bool hilighted)
    {
        renderer.sharedMaterial = hilighted ? hilightedMaterial : basicMaterial;

    }
}
