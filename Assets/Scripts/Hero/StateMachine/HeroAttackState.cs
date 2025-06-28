using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAttackState : HeroBaseState
{
    public override void EnterState(HeroStateManager state, Animator animator)
    {
        base.EnterState(state, animator);

        if (animator != null)
        {
            animator.SetTrigger(TrAttack);
        }
    }

    public override void UpdateState(HeroStateManager state)
    {
        base.UpdateState(state);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            state.SwitchState(state.JumpState);
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            state.SwitchState(state.DashState);
            return;
        }
    }

    public override void OnTriggerEnter2D(Collider2D coll, HeroStateManager state, Animator animator)
    {
        base.OnTriggerEnter2D(coll, state, animator);
    }
}
