using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDashState : HeroBaseState
{
    private float _dir;
    private float _dashSpeed = 7f;
    public override void EnterState(HeroStateManager state, Animator animator)
    {

        base.EnterState(state, animator);

        _dir = _hero.transform.rotation.y == 0 ? 1f : -1f;

        if (animator != null)
        {
            animator.SetTrigger(TrDash);
        }
    }

    public override void UpdateState(HeroStateManager state)
    {
        _hero.transform.Translate(new Vector2(_dir, 0f) * (state.dashSpeed * Time.deltaTime), Space.World);
    }

    public override void OnTriggerEnter2D(Collider2D coll, HeroStateManager state, Animator animator)
    {
        base.OnTriggerEnter2D(coll, state, animator);
    }
}
