using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInAirState : HeroBaseState
{
    public override void EnterState(HeroStateManager state, Animator animator)
    {

    }

    public override void UpdateState(HeroStateManager state)
    {
        base.UpdateState(state);
    }
    public override void OnTriggerEnter2D(Collider2D coll, HeroStateManager state, Animator animator)
    {
        base.OnTriggerEnter2D(coll, state, animator);
    }


}
