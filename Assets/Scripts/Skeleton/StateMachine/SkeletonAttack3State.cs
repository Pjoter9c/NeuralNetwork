using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class SkeletonAttack3State : SkeletonBaseState
{
    private float t;
    public override void EnterState(SkeletonStateManager state, Animator animator)
    {
        t = 0f;

        animator.ResetTrigger(TrAttack1);
        animator.ResetTrigger(TrAttack2);
        animator.SetBool(IsIdle, false);
        animator.SetBool(IsWalk, false);
        animator.SetTrigger(TrAttack3);

        state._canvasAttacks.transform.GetChild(2).GetComponent<FillSkills>().ResetCooldown();

    }

    public override void UpdateState(SkeletonStateManager state)
    {
        if(t <= 1f)
        {
            state.FlamesMaterial.SetFloat("_Fade", t);
            t = Mathf.Clamp01(t + Time.deltaTime * 2f);
        }
    }
}
