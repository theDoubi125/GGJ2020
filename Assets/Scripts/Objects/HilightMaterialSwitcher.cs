using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class HilightMaterialSwitcher: MonoBehaviour
{
    private Material basicMaterial;
    public Material hilightedMaterial;
    public int materialIndex = 0;
    public new Renderer renderer;

    void Start()
    {
        GetComponent<Interactable>().hilightChangedDelegate += OnHilightChanged;
        if(renderer == null)
        {
            renderer = GetComponent<Renderer>();
        }
        basicMaterial = renderer.materials[materialIndex];
        
    }

    public void OnHilightChanged(bool hilighted)
    {
        Material[] materials = renderer.sharedMaterials;
        materials[materialIndex] = hilighted ? hilightedMaterial : basicMaterial;
        renderer.sharedMaterials = materials;

    }
}
