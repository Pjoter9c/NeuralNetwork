using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroBaseState
{
    public virtual void EnterState(HeroStateManager state, Animator animator)
    {
        _hero = state.gameObject;
    }
    public virtual void UpdateState(HeroStateManager state, bool[] actions)
    {
        //_horizontal = Input.GetAxis("Horizontal");
    }
    public virtual void OnTriggerEnter2D(Collider2D coll, HeroStateManager state, Animator animator)
    {
        if(coll == null) return;
        if (coll.gameObject.CompareTag("SkeletonDmg") && state.CurrentState != state.DeadState)
        {
            animator.SetTrigger("TrDead");

            state.SwitchState(state.DeadState);
        }
    }



    protected static readonly int IsIdle = Animator.StringToHash("IsIdle");
    protected static readonly int IsRunning = Animator.StringToHash("IsRunning");
    protected static readonly int IsInAir = Animator.StringToHash("IsInAir");
    protected static readonly int TrAttack = Animator.StringToHash("TrAttack");
    protected static readonly int TrJump = Animator.StringToHash("TrJump");
    protected static readonly int TrDash = Animator.StringToHash("TrDash");
    protected static readonly int TrDead = Animator.StringToHash("TrDead");

    protected float _horizontal;

    protected GameObject _hero;
}
