using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkeletonBaseState
{
    protected static readonly int IsIdle = Animator.StringToHash("IsIdle");
    protected static readonly int IsWalk = Animator.StringToHash("IsWalking");
    protected static readonly int TrDead = Animator.StringToHash("TrDead");
    protected static readonly int TrAttack1 = Animator.StringToHash("TrAttack1");
    protected static readonly int TrAttack2 = Animator.StringToHash("TrAttack2");
    protected static readonly int TrAttack3 = Animator.StringToHash("TrAttack3");

    protected float _horizontal;

    public virtual void EnterState(SkeletonStateManager state, Animator animator) { }

    public virtual void UpdateState(SkeletonStateManager state)
    {
        _horizontal = Input.GetAxis("Horizontal");
    }
}
