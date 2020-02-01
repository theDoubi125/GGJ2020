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

    private void OnInteraction(Hand hand)
    {
        if(!hand.toolHandler.hasTool)
        {
            hand.toolHandler.SetTool(inHandVersionPrefab);
            Destroy(gameObject);
        }
    }
}
