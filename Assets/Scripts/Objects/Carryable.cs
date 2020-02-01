using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grabbable))]
[RequireComponent(typeof(CarryJoint))]
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

    void Start()
    {
        grabbable = GetComponent<Grabbable>();
        grabbable.grabbedDelegate += OnGrabbed;
        grabbable.startReleaseDelegate += OnStartRelease;
        grabbable.releasedDelegate += OnReleased;
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
    }

    private void OnReleased()
    {
        inGrabAnimation = false;
    }

    private void Update()
    {
        if(inGrabAnimation)
        {
            carryAnimationTime += Time.deltaTime;
            transform.position = attachedHand.transform.position + grabAnimationOffset + Vector3.up * carryAnimationTime / carryAnimationDuration;
            if(carryAnimationTime > carryAnimationDuration)
            {
                inGrabAnimation = false;
                fixedJoint = gameObject.AddComponent<FixedJoint>();
                fixedJoint.connectedBody = attachedHand.grabJointTarget;
            }
        }
    }

    private void OnGrabAnimationFinished()
    {
        attachedArm.animationFinishedDelegate -= OnGrabAnimationFinished;
        //inGrabAnimation = false;
    }
}
