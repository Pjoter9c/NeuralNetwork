using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDeadState : HeroBaseState
{
    public override void EnterState(HeroStateManager state, Animator animator)
    {
        base.EnterState(state, animator);
        animator.SetBool(IsIdle, false);

        if (animator != null)
        {
            animator.SetTrigger(TrDead);
        }
    }

    public override void UpdateState(HeroStateManager state, bool[] actions)
    {
        base.UpdateState(state, actions);
        if (Input.GetKeyDown(KeyCode.R))
        {
            
            state.SwitchState(state.IdleState);
            return;
        }
    }
    public override void OnTriggerEnter2D(Collider2D coll, HeroStateManager state, Animator animator)
    {
        base.OnTriggerEnter2D(coll, state, animator);
    }
}
