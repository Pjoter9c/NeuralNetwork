using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class HeroAttackState : HeroBaseState
{
    public override void EnterState(HeroStateManager state, Animator animator)
    {
        base.EnterState(state, animator);

        if (animator != null)
        {
            animator.ResetTrigger(TrJump);
            animator.ResetTrigger(TrDash);
            animator.ResetTrigger(TrDead);
            animator.SetTrigger(TrAttack);
        }
    }

    public override void UpdateState(HeroStateManager state, bool[] actions)
    {
        base.UpdateState(state, actions);

        if (actions[5])
        {
            state.SwitchState(state.JumpState);
            return;
        }
        if (actions[3] || actions[4])
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
