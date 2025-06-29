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

    public override void UpdateState(HeroStateManager state)
    {
        base.UpdateState(state);

        //switch to attack
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            state.SwitchState(state.AttackState);
            return;
        }

        // switch to dash
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            state.SwitchState(state.DashState);
            return;
        }

        // switch to idle
        if (_horizontal == 0)
        {
            state.SwitchState(state.IdleState);
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

        // walking
        _hero = state.gameObject;

        _hero.transform.Translate(Vector2.right * (state.speed * Time.deltaTime));

        float angle = 90f - 90f * Mathf.Sign(_horizontal);
        _hero.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        
    }
    public override void OnTriggerEnter2D(Collider2D coll, HeroStateManager state, Animator animator)
    {
        base.OnTriggerEnter2D(coll, state, animator);
    }

}
