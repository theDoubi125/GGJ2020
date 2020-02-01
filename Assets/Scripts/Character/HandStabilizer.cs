using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandStabilizer : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 lookDirection = transform.forward;
        lookDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDirection.normalized, Vector3.up);   
    }
}
