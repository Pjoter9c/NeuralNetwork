using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : SkeletonBaseState
{
    public override void EnterState(SkeletonStateManager state, Animator animator)
    {
        state.HeroInfo.SetEnemyAttackType(0);

        animator.ResetTrigger(TrAttack1);
        animator.ResetTrigger(TrAttack2);
        animator.ResetTrigger(TrAttack3);
        animator.SetBool(IsIdle, true);
        animator.SetBool(IsWalk, false);
    }

    public override void UpdateState(SkeletonStateManager state)
    {
        base.UpdateState(state);

        if (_horizontal != 0f)
        {
            state.SwitchState(state.WalkState);
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
    }
}
