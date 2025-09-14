using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDeadState : SkeletonBaseState
{
    public override void EnterState(SkeletonStateManager state, Animator animator)
    {

        animator.ResetTrigger(TrAttack1);
        animator.ResetTrigger(TrAttack2);
        animator.ResetTrigger(TrAttack3);
        animator.SetBool(IsIdle, false);
        animator.SetBool(IsWalk, false);

        animator.SetTrigger(TrDead);
    }
}
