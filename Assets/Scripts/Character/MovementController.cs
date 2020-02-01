using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    public int playerIndex = 1;
    public Vector2 input;
    public float movementForce;
    public float turnSpeed;
    private Rigidbody rigidbody;

    public Transform spawnPoint;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        this.transform.position = spawnPoint.position;
    }

    void Update()
    {
        input.x = Input.GetAxis("Horizontal" + playerIndex);
        input.y = Input.GetAxis("Vertical" + playerIndex);
        Vector3 inputDirection = new Vector3(input.x, 0, input.y);
        if(inputDirection.magnitude > 1)
            inputDirection = inputDirection.normalized;
        rigidbody.AddForce(inputDirection * movementForce);
        if(Vector3.Dot(transform.forward, inputDirection) < 0)
        {
            rigidbody.AddTorque(Vector3.Cross(transform.forward, inputDirection.normalized).normalized * turnSpeed);

        }
        else 
            rigidbody.AddTorque(Vector3.Cross(transform.forward, inputDirection.normalized) * turnSpeed);
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
