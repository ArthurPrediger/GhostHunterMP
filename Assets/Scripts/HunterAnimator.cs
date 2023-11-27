using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterAnimator : MonoBehaviour
{
    public Animator animatior;

    public void PlayerAnimationController(string animationName)
    {
        animatior.Play(animationName);
    }
}
