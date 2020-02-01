using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    public Vector2 input;
    public float movementForce;
    public float turnSpeed;
    private Rigidbody rigidbody;


    public Ship currentShip;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        Vector3 inputDirection = new Vector3(input.x, 0, input.y);
        if(inputDirection.magnitude > 1)
            inputDirection = inputDirection.normalized;
        rigidbody.AddForce(inputDirection * movementForce);
        rigidbody.AddTorque(Vector3.Cross(transform.forward, inputDirection).normalized * turnSpeed);
    }

    private void OnTriggerStay(Collider other)
    {
        //if(Input.GetButtonDown("ContextualAction"))
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("YOU ARE TRYING TO CATCH  : " + other.name);
            //currentShip.RemoveShipPart(other.name);
        }
    }

}
