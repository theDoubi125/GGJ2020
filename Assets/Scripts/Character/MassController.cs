using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MovementController))]
public class MassController : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private MovementController movementController;
    public MassConfig defaultConfig;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        movementController = GetComponent<MovementController>();
        SetMassConfig(defaultConfig);
    }

    void SetMassConfig(MassConfig massConfig)
    {
        rigidbody.drag = massConfig.drag;
        rigidbody.angularDrag = massConfig.angularDrag;
        movementController.movementForce = massConfig.movementForce;
        movementController.turnSpeed = massConfig.turnSpeed;
    }
}
