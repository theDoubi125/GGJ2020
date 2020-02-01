using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachPart : MonoBehaviour
{
    private Interactable interactable;
    public Transform toSpawn;

    void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.interactDelegate += OnInteraction;
    }

    private void OnInteraction(Interactable interactable, Hand hand, Tool.ToolType toolType)
    {
        if(toolType == Tool.ToolType.Wrench)
        {
            Instantiate(toSpawn, transform.position, transform.rotation);
            gameObject.SetActive(false);
        }
    }
    
    void Update()
    {
        
    }
}
