using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedTool : MonoBehaviour
{
    public Transform inHandVersionPrefab;

    private Interactable interactable;

    void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.interactDelegate += OnInteraction;
    }

    private void OnInteraction(Interactable interactable, Hand hand, Tool.ToolType toolType)
    {
        if(toolType == Tool.ToolType.None)
        {
            hand.toolHandler.SetTool(inHandVersionPrefab);
            Destroy(gameObject);
        }
    }
}
