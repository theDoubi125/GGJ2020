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

    private void OnInteraction(Hand hand)
    {
        Instantiate(toSpawn, transform.position, transform.rotation);

        gameObject.SetActive(false);
    }
    
    void Update()
    {
        
    }
}
