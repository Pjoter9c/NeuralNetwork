using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class HeroJumpState : HeroBaseState
{
    Rigidbody2D rb;
    public override void EnterState(HeroStateManager state, Animator animator)
    {
        base.EnterState(state, animator);
        animator.ResetTrigger(TrAttack);
        animator.ResetTrigger(TrDash);
        animator.ResetTrigger(TrDead);
        animator.SetBool(IsIdle, false);
        animator.SetBool(IsRunning, false);
        animator.SetBool(IsInAir, true);

        rb = _hero.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, 25f);

        if (animator != null)
        {
            animator.SetTrigger(TrJump);
        }
    }

    public override void UpdateState(HeroStateManager state, bool[] actions)
    {
        base.UpdateState(state, actions);
        
        if (state.IsGrounded && state.Jumped)
        {
            state.Jumped = false;
            state.SwitchState(state.IdleState);
        }
    }

    public override void OnTriggerEnter2D(Collider2D coll, HeroStateManager state, Animator animator)
    {
        base.OnTriggerEnter2D(coll, state, animator);
    }
}
