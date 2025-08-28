using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttack1State : SkeletonBaseState
{
    public override void EnterState(SkeletonStateManager state, Animator animator)
    {
        animator.ResetTrigger(TrAttack2);
        animator.ResetTrigger(TrAttack3);
        animator.SetBool(IsWalk, false);
        animator.SetBool(IsIdle, false);
        animator.SetTrigger(TrAttack1);

        state._canvasAttacks.transform.GetChild(0).GetComponent<FillSkills>().ResetCooldown();
    }

    public override void UpdateState(SkeletonStateManager state)
    {
        if (Time.timeScale == 0f)
            return;
    }
}
