using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDeadState : HeroBaseState
{

    float t;
    public override void EnterState(HeroStateManager state, Animator animator)
    {
        var info = state.gameObject.GetComponent<HeroInfo>().GetHeroInfo();
        var actions = state.GetActions();

        base.EnterState(state, animator);
        animator.ResetTrigger(TrAttack);
        animator.ResetTrigger(TrDash);
        animator.ResetTrigger(TrJump);
        animator.SetBool(IsIdle, false);
        animator.SetBool(IsRunning, false);
        animator.SetBool(IsInAir, false);

        state.GetComponent<HeroInfo>().Kills++;

        if (animator != null)
        {
            animator.SetTrigger(TrDead);
        }

        // learn new move
        var moves = state.trainingSample[(int)info[0]];
        for (int i = 0; i < moves.Count; i++)
        {
            var move = moves[i];
            // learn first unlearned move for this attack type
            if (move.learned == 0)
            {
                Data data = new Data();
                data.attackType = move.attackType;
                data.side = move.side;
                data.orientation = move.orientation;
                data.distance = move.distance;
                data.inDmg = move.inDmg;
                data.action = move.action;
                data.learned = 1;

                state.trainingSample[(int)info[0]][i] = data;
                break;
            }
        }
        t = -1f;
    }

    public override void UpdateState(HeroStateManager state, bool[] actions)
    {
        base.UpdateState(state, actions);
        
        // resurect effect
        if(t < 1f)
        {
            t += (Time.deltaTime * 0.5f);
            state.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_blend", t);
        }
        else
        {
            state.gameObject.transform.position = new Vector2(-11f, -3.1f);
            state.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_blend", 0f);
            state.gameObject.GetComponent<HeroNeuralNetwork>().StartLearning();
            state.SwitchState(state.IdleState);
            return;
        }
    }
    public override void OnTriggerEnter2D(Collider2D coll, HeroStateManager state, Animator animator)
    {
        base.OnTriggerEnter2D(coll, state, animator);
    }
}
