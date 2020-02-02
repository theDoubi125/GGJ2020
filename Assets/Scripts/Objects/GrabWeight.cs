using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grabbable))]
public class GrabWeight : MonoBehaviour
{
    private Grabbable grabbable;
    public MassConfig massConfig;
    private Hand grabbingHand;
    private bool isWeightActive = false;
    private MassController massController;

    void Start()
    {
        grabbable = GetComponent<Grabbable>();    
        grabbable.grabbedDelegate += OnGrabbed;
        grabbable.releasedDelegate += OnReleased;
    }

    private void OnGrabbed(Hand hand)
    {
        grabbingHand = hand;
        massController = hand.GetComponentInParent<MassController>();
        massController.SetMassConfig(massConfig);
        isWeightActive = true;
    }

    private void OnReleased()
    {
        massController = grabbingHand.GetComponentInParent<MassController>();
        massController.SetMassConfig(massController.defaultConfig);
        isWeightActive = false;
    }
    private void OnDestroy()
    {
        if(isWeightActive)
            massController.SetMassConfig(massController.defaultConfig);

    }

}
