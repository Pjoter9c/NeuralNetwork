using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SkeletonWalkState : SkeletonBaseState
{
    public override void EnterState(SkeletonStateManager state, Animator animator)
    {
        state.HeroInfo.SetEnemyAttackType(0);

        animator.ResetTrigger(TrAttack1);
        animator.ResetTrigger(TrAttack2);
        animator.ResetTrigger(TrAttack3);
        animator.SetBool(IsWalk, true);
        animator.SetBool(IsIdle, false);
    }

    public override void UpdateState(SkeletonStateManager state)
    {
        base.UpdateState(state);
      
        if (_horizontal == 0f)
        {
            state.SwitchState(state.IdleState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && state.Attack1Image.fillAmount >= 1f)
        {
            state.SwitchState(state.Attack1State);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && state.Attack2Image.fillAmount >= 1f)
        {
            state.SwitchState(state.Attack2State);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && state.Attack3Image.fillAmount >= 1f)
        {
            state.SwitchState(state.Attack3State);
            return;
        }

        state.gameObject.transform.Translate(Vector2.left * (state.Speed * Time.deltaTime));

        float angle = 90f + 90f * Mathf.Sign(_horizontal);
        state.gameObject.transform.rotation = Quaternion.Euler(0f, angle, 0f);

        //Debug.Log($"{_horizontal} {angle}");
    }
}
