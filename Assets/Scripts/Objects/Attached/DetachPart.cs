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

        Instantiate(toSpawn, transform.position, transform.rotation);

        gameObject.SetActive(false);

        if(toolType == Tool.ToolType.Wrench)
        {
            Transform spawned = Instantiate(toSpawn, transform.position, transform.rotation);
            spawned.GetComponent<Repairable>().currentState = GetComponent<Repairable>().currentState;
            gameObject.SetActive(false);
        }
    }
    
    void Update()
    {
        
    }
}
