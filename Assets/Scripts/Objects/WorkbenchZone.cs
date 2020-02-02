using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if(interactable != null)
            interactable.interactDelegate += OnInteract;
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if(interactable != null)
            interactable.interactDelegate -= OnInteract;
    }

    public void OnInteract(Interactable interactable, Hand hand, Tool.ToolType toolType)
    {
        Repairable repairable = interactable.GetComponent<Repairable>();
        if(repairable != null)
        {
            repairable.RepairWithTool(toolType);
        }
    }
}
