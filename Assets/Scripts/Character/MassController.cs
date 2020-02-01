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
    public MassConfig currentConfig;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        movementController = GetComponent<MovementController>();
        currentConfig = defaultConfig;
        SetMassConfig(defaultConfig);
    }

    private void Update()
    {
        SetMassConfig(currentConfig);
    }

    public void SetMassConfig(MassConfig massConfig)
    {
        currentConfig = massConfig;
        rigidbody.drag = massConfig.drag;
        rigidbody.angularDrag = massConfig.angularDrag;
        movementController.movementForce = massConfig.movementForce;
        movementController.turnSpeed = massConfig.turnSpeed;
    }
}
