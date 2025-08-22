using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttack2State : SkeletonBaseState
{
    private Vector2 _startPos, _endPos;
    private float _x, _xPos, t;

    public override void EnterState(SkeletonStateManager state, Animator animator)
    {
        animator.ResetTrigger(TrAttack1);
        animator.ResetTrigger(TrAttack3);
        animator.SetBool(IsIdle, false);
        animator.SetBool(IsWalk, false);
        animator.SetTrigger(TrAttack2);

        state._canvasAttacks.transform.GetChild(1).GetComponent<FillSkills>().ResetCooldown();


        _startPos = state.gameObject.transform.position;
        _endPos = _startPos - (Vector2)state.gameObject.transform.right * 3f;
        _xPos = _startPos.x;
        _x = 0f;
        t = 0f;
    }

    public override void UpdateState(SkeletonStateManager state)
    {
        if (t < 1f)
        {
            t += Time.deltaTime * 2f;
            _x = EaseInBack(t);
            _xPos = Mathf.LerpUnclamped(_startPos.x, _endPos.x, _x);
            state.gameObject.transform.position = new Vector2(_xPos, _startPos.y);
        }
    }

    private float EaseInBack(float x) 
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1f;

        return c3* x * x* x - c1* x * x;
    }
}
