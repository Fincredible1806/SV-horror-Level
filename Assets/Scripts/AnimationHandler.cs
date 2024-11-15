using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private string animationTrigger;

    public void SetTrigger()
    {
        animator.SetTrigger(animationTrigger);
    }
}
