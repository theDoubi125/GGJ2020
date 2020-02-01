using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grabbable))]
public class GrabJoint : MonoBehaviour
{
    void Start()
    {
        GetComponent<Grabbable>().grabbedDelegate += OnGrabbed;
        GetComponent<Grabbable>().releasedDelegate += OnReleased;

    }

    private void OnGrabbed(Hand hand)
    {
        FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = hand.GetComponentInParent<Rigidbody>();

    }

    private void OnReleased(Hand hand)
    {
        Destroy(gameObject.GetComponent<FixedJoint>());
    }

    void Update()
    {
        
    }
}
