using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroIdleState : HeroBaseState
{
    public override void EnterState(HeroStateManager state, Animator animator)
    {
        if (animator != null)
        {
            animator.SetBool(IsIdle, true);
            animator.SetBool(IsRunning, false);
            animator.SetBool(IsInAir, false);
        }
    }

    public override void UpdateState(HeroStateManager state, bool[] actions)
    {
        //_horizontal = Input.GetAxis("Horizontal");
        if (actions[1] || actions[2])
        {
            state.SwitchState(state.RunState);
            return;
        }
        if (actions[3] || actions[4])
        {
            state.SwitchState(state.DashState);
            return;
        }
        if (actions[5])
        {
            state.SwitchState(state.JumpState);
            return;
        }
        if (actions[6])
        {
            state.SwitchState(state.AttackState);
            return;
        }
    }
    public override void OnTriggerEnter2D(Collider2D coll, HeroStateManager state, Animator animator)
    {
        base.OnTriggerEnter2D(coll, state, animator);
    }


}
