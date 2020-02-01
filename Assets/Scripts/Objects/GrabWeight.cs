using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grabbable))]
public class GrabWeight : MonoBehaviour
{
    private Grabbable grabbable;
    public MassConfig massConfig;

    void Start()
    {
        grabbable = GetComponent<Grabbable>();    
        grabbable.grabbedDelegate += OnGrabbed;
        grabbable.releasedDelegate += OnReleased;
    }

    private void OnGrabbed(Hand hand)
    {
        MassController massController = hand.GetComponentInParent<MassController>();
        massController.SetMassConfig(massConfig);
    }

    private void OnReleased(Hand hand)
    {
        MassController massController = hand.GetComponentInParent<MassController>();
        massController.SetMassConfig(massController.defaultConfig);
    }

    void Update()
    {
        
    }
}
