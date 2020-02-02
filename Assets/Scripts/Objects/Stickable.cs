using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stickable : MonoBehaviour
{
    private new Rigidbody rigidbody;
    public float gravity;
    private FixedJoint fixedJoint;
    public void SetSticked(StickyZone stickyZone)
    {
        if(fixedJoint != null)
            Destroy(fixedJoint);
        fixedJoint = gameObject.AddComponent<FixedJoint>();
        fixedJoint.connectedBody = stickyZone.GetComponent<Rigidbody>();
        GetComponent<Grabbable>().grabbedDelegate += OnGrabbed;
    }

    private void OnGrabbed(Hand hand)
    {
        if(fixedJoint != null)
        {
            Destroy(fixedJoint);
            fixedJoint = null;
        }
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called  per frame
    void Update()
    {
        rigidbody.AddForce(Vector3.down * gravity);
        
    }
}
