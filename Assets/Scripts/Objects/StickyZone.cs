using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Stickable stickable = other.GetComponent<Stickable>();
        if(stickable != null && !other.isTrigger)
        {
            stickable.SetSticked(this);
        }
    }
}
