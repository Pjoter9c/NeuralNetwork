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

    public override void UpdateState(HeroStateManager state)
    {
        _horizontal = Input.GetAxis("Horizontal");
        if (_horizontal != 0)
        {
            state.SwitchState(state.RunState);
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            state.SwitchState(state.DashState);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            state.SwitchState(state.JumpState);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
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
