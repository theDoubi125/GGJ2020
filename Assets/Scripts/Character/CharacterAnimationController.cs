using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator animator;
    private Hand hand;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        hand = GetComponentInChildren<Hand>();
    }

    void Update()
    {
        animator.SetBool("CanGrab", hand.objectInRange != null);
        animator.SetBool("Grab", hand.grabbedObject != null);
    }
}
