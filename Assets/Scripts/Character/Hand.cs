using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public float grabRange;
    public LayerMask grabLayerMask;
    public Interactable objectInRange;
    public Grabbable grabbedObject;
    public Transform[] raycastSources;
    public Rigidbody grabJointTarget;
    private Interactable smallInteractableInRange;

    public ToolHandler toolHandler;
    public int playerIndex = 1;

    void Update()
    {
        RaycastHit raycastHit;
        Interactable hoveredInteractable = null;
        if(smallInteractableInRange != null)
        {
            hoveredInteractable = smallInteractableInRange;
        }
        else
        {
            foreach(Transform raycastSource in raycastSources)
            {
                if(Physics.Raycast(raycastSource.position, raycastSource.forward, out raycastHit, grabRange, grabLayerMask))
                {
                    hoveredInteractable = raycastHit.collider.GetComponent<Interactable>();
                    if(hoveredInteractable != null)
                        break;
                }
            }
        }

        if(hoveredInteractable != null)
        {
            if(objectInRange != hoveredInteractable)
            {
                if(objectInRange != null)
                {
                    objectInRange.SetHilight(false);
                }
                else
                {
                    objectInRange = hoveredInteractable;
                    objectInRange.SetHilight(true);
                }
            }
        }
        else if(objectInRange != null)
        {
            objectInRange.SetHilight(false);
            objectInRange = null;
        }

        if(Input.GetButtonDown("Interact" + playerIndex))
        {
            if(grabbedObject == null)
            {
                if(objectInRange != null)
                {
                    objectInRange.OnInteractionBy(this, toolHandler.usedToolType);
                }
            }
            else
            {
                if(objectInRange != null)
                {
                    ShipSocket socket = objectInRange.GetComponent<ShipSocket>();
                    ShipElement element = grabbedObject.GetComponent<ShipElement>();
                    if(element != null && socket != null && element.shipPart == socket.shipPart)
                    {
                        socket.FillSocket(element);
                    }


                }
                grabbedObject.OnReleased(this);
                grabbedObject = null;
            }
        }

        if(Input.GetButtonDown("Drop" + playerIndex))
        {
            if(toolHandler.hasTool)
            {
                toolHandler.DropTool();
            }
            if(grabbedObject == null)
            {

            }
        }
    }

    public void GrabObject(Grabbable grabbable)
    {
        grabbedObject = grabbable;
        grabbable.OnGrabbed(this);
    }
    private void OnTriggerEnter(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if(interactable != null && interactable.isSmall)
        {
            smallInteractableInRange = interactable;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if(interactable != null && interactable == smallInteractableInRange)
        {
            smallInteractableInRange = null;
        }
    }
}
