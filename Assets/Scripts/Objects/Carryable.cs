using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grabbable))]
public class Carryable : MonoBehaviour
{
    private Grabbable grabbable;
    private CarryJoint carryJoint;
    private Arm attachedArm;
    private Hand attachedHand;
    private bool inGrabAnimation = false;
    private Vector3 grabAnimationOffset;
    public float carryHeight = 1;
    public float carryAnimationDuration = 0.5f;
    private float carryAnimationTime = 0;
    private FixedJoint fixedJoint;
    private Rigidbody body;
    private float wakeupDuration = 1;
    private float wakeupTime = 0;

    void Start()
    {
        grabbable = GetComponent<Grabbable>();
        grabbable.grabbedDelegate += OnGrabbed;
        grabbable.startReleaseDelegate += OnStartRelease;
        grabbable.releasedDelegate += OnReleased;
        body = GetComponent<Rigidbody>();
    }

    private void OnGrabbed(Hand hand)
    {
        attachedHand = hand;
        inGrabAnimation = true;
        carryAnimationTime = 0;
        grabAnimationOffset = transform.position - attachedHand.transform.position;
    }

    private void OnStartRelease()
    {
        Destroy(fixedJoint);
        grabAnimationOffset = transform.position - attachedHand.transform.position;
        wakeupTime = 0;
    }

    private void OnReleased()
    {
        inGrabAnimation = false;
    }

    private void Update()
    {
        wakeupTime += Time.deltaTime;
        if(body.IsSleeping() && wakeupTime < wakeupDuration)
        {
            body.WakeUp();
        }
        if(inGrabAnimation)
        {
            carryAnimationTime += Time.deltaTime;
            transform.position = attachedHand.transform.position + grabAnimationOffset + (-grabAnimationOffset + Vector3.up * carryHeight) * carryAnimationTime / carryAnimationDuration;
            if(carryAnimationTime > carryAnimationDuration)
            {
                inGrabAnimation = false;
                fixedJoint = gameObject.AddComponent<FixedJoint>();
                fixedJoint.connectedBody = attachedHand.grabJointTarget;
                fixedJoint.connectedMassScale = 0.01f;
                
                body.WakeUp();
            }
        }
    }

    private void OnGrabAnimationFinished()
    {
        //inGrabAnimation = false;
    }
}
