using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Arm : MonoBehaviour
{
    public float baseAngle = -90;
    public float grabAngle = 0;
    public float carryAngle = 90;
    public float rotationSpeed = 90;
    public float releaseAngle = -45;
    public float carryHeight = 1;

    private float currentAngle;
    private float targetAngle;

    private Hand hand;
    private Grabbable carriedObject;

    public Action animationFinishedDelegate;

    void Start()
    {
        hand = GetComponentInChildren<Hand>();
        targetAngle = baseAngle;
    }

    void FixedUpdate()
    {
        transform.localRotation = Quaternion.Euler(currentAngle, 0, 0);        
        if(currentAngle > targetAngle)
        {
            currentAngle -= rotationSpeed * Time.fixedDeltaTime;
            if(currentAngle < targetAngle)
            {
                currentAngle = targetAngle;
                if(animationFinishedDelegate != null)
                    animationFinishedDelegate();
            }
        }
        if(currentAngle < targetAngle)
        {
            currentAngle += rotationSpeed * Time.fixedDeltaTime;
            if(currentAngle > targetAngle)
            {
                currentAngle = targetAngle;
                if(animationFinishedDelegate != null)
                    animationFinishedDelegate();
            }
        }
    }

    public void StartCarry(Grabbable grabbedObject)
    {
        targetAngle = carryAngle;
        carriedObject = grabbedObject;
        carriedObject.canReleaseDelegate += CanReleaseGrabbedObject;
        carriedObject.startReleaseDelegate += OnStartRelease;
    }

    private void OnStartRelease() 
    {
        targetAngle = baseAngle;
    }

    private bool CanReleaseGrabbedObject()
    {
        return currentAngle > releaseAngle;
    }
}
