using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttack1State : SkeletonBaseState
{
    public override void EnterState(SkeletonStateManager state, Animator animator)
    {
        animator.SetTrigger(TrAttack1);
        animator.SetBool(IsIdle, false);
        animator.SetBool(IsWalk, false);

        state._canvasAttacks.transform.GetChild(0).GetComponent<FillSkills>().ResetCooldown();
    }

    public override void UpdateState(SkeletonStateManager state)
    {
       
    }
}
