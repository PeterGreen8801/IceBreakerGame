using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string DROWN = "Drown";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //Plays the Drown animation
    public void playDrownAnimation()
    {
        animator.Play(DROWN);
    }
}
