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
        rb = _hero.GetComponent<Rigidbody2D>();

        state.WaitTime(1f);
        if (animator != null)
        {
            animator.SetTrigger(TrJump);
        }
        rb.velocity = new Vector2(rb.velocity.x, 25f);
    }

    public override void UpdateState(HeroStateManager state, bool[] actions)
    {
        base.UpdateState(state, actions);
        
        if (state.IsGrounded)
        {
            state.SwitchState(state.IdleState);
        }
    }

    public override void OnTriggerEnter2D(Collider2D coll, HeroStateManager state, Animator animator)
    {
        base.OnTriggerEnter2D(coll, state, animator);
    }
}
