using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class HeroRunState : HeroBaseState
{
    public override void EnterState(HeroStateManager state, Animator animator)
    {
        if (animator != null)
        {
            animator.SetBool(IsRunning, true);
            animator.SetBool(IsIdle, false);
            animator.SetBool(IsInAir, false);
        }
    }

    public override void UpdateState(HeroStateManager state, bool[] actions)
    {
        base.UpdateState(state, actions);

        //switch to attack
        if (actions[6])
        {
            state.SwitchState(state.AttackState);
            return;
        }

        // switch to dash
        if (actions [3] || actions[4])
        {
            state.SwitchState(state.DashState);
            return;
        }

        // switch to idle
        if (actions[0])
        {
            state.SwitchState(state.IdleState);
            return;
        }
        if (actions[5])
        {
            state.SwitchState(state.JumpState);
            return;
        }

        // walking
        _hero = state.gameObject;

        _hero.transform.Translate(Vector2.right * (state.speed * Time.deltaTime));
        float side = 0f;
        if (actions[1])
            side = -1f;
        if (actions[2])
            side = 1f;
        float angle = 90f - 90f * side;
        _hero.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        
    }
    public override void OnTriggerEnter2D(Collider2D coll, HeroStateManager state, Animator animator)
    {
        base.OnTriggerEnter2D(coll, state, animator);
    }

}
