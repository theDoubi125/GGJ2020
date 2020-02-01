using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public float grabRange;
    public LayerMask grabLayerMask;
    public Interactable objectInRange;
    public Grabbable grabbedObject;
    public Transform raycastSource;
    public Rigidbody grabJointTarget;

    void Update()
    {
        RaycastHit raycastHit;
        if(Physics.Raycast(raycastSource.position, raycastSource.forward, out raycastHit, grabRange, grabLayerMask))
        {
            Interactable interactable = raycastHit.collider.GetComponent<Interactable>();
            if(objectInRange != interactable)
            {
                if(objectInRange != null)
                {
                    objectInRange.SetHilight(false);
                }
                else
                {
                    objectInRange = raycastHit.collider.GetComponent<Interactable>();
                    objectInRange.SetHilight(true);
                }
            }
        }
        else if(objectInRange != null)
        {
            objectInRange.SetHilight(false);
            objectInRange = null;
        }

        if(Input.GetButtonDown("Interact"))
        {
            if(grabbedObject == null)
            {
                if(objectInRange != null)
                {
                    objectInRange.OnInteractionBy(this);
                }
            }
            else
            {
                grabbedObject.OnReleased(this);
                grabbedObject = null;
            }
        }
    }

    public void GrabObject(Grabbable grabbable)
    {
        grabbedObject = grabbable;
        grabbable.OnGrabbed(this);
    }
}
