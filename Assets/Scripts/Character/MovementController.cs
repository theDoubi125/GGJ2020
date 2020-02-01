﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    public Vector2 input;
    public float movementForce;
    public float turnSpeed;
    private Rigidbody rigidbody;

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
        if(Vector3.Dot(transform.forward, inputDirection) < 0)
        {
            rigidbody.AddTorque(Vector3.Cross(transform.forward, inputDirection.normalized).normalized * turnSpeed);

        }
        else 
            rigidbody.AddTorque(Vector3.Cross(transform.forward, inputDirection.normalized) * turnSpeed);
    }
}
