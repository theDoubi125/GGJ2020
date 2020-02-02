using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class HilightAllMaterialsSwitcher: MonoBehaviour
{
    private Material[] baseMaterials;
    public Material hilightedMaterial;
    public new Renderer renderer;

    void Start()
    {
        GetComponentInParent<Interactable>().hilightChangedDelegate += OnHilightChanged;
        if(renderer == null)
            renderer = GetComponent<Renderer>();
        
    }

    public void OnHilightChanged(bool hilighted)
    {
        if(hilighted)
        {
            baseMaterials = new Material[renderer.materials.Length];
            for(int i=0; i< renderer.materials.Length; i++)
            {
                baseMaterials[i] = renderer.materials[i];
                renderer.sharedMaterials[i]= hilightedMaterial;
            }

        }
        else
        {
            for(int i=0; i< renderer.materials.Length; i++)
            {
                renderer.sharedMaterials[i]= baseMaterials[i];
            }

        }

    }
}
