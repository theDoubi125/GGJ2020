using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MassConfig : ScriptableObject
{
    public float drag = 5;
    public float angularDrag = 10;
    public float movementForce = 15;
    public float turnSpeed = 0.5f;
}
