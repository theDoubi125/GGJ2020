using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Repairable repairable= other.GetComponent<Repairable>();
        if(repairable != null && !other.isTrigger)
        {
            repairable.Repaint();
        }
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