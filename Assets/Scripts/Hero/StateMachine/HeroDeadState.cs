using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDeadState : HeroBaseState
{

    float val;
    public override void EnterState(HeroStateManager state, Animator animator)
    {
        var info = state.gameObject.GetComponent<HeroInfo>().GetHeroInfo();
        var actions = state.GetActions();
        
        /*  Write info to text file
        string inVal = $"In: {info[0]} {info[1]} {info[2]} {info[3]} {info[4]}";
        string outVal = $"Out: {actions[0]} {actions[1]} {actions[2]} {actions[3]} {actions[4]} {actions[5]} {actions[6]}";
        state.gameObject.GetComponent<WriteData>().WriteLine(inVal);
        state.gameObject.GetComponent<WriteData>().WriteLine(outVal);
        */

        base.EnterState(state, animator);
        animator.ResetTrigger(TrAttack);
        animator.ResetTrigger(TrDash);
        animator.ResetTrigger(TrJump);
        animator.SetBool(IsIdle, false);
        animator.SetBool(IsRunning, false);
        animator.SetBool(IsInAir, false);

        if (animator != null)
        {
            animator.SetTrigger(TrDead);
        }
        //Debug.Log("Dead");

        // learn new move
        var moves = state.trainingSample[(int)info[0]];
        for (int i = 0; i < moves.Count; i++)
        {
            var move = moves[i];
            Debug.Log($"ToLearn {move.attackType} {move.side} {move.orientation} {move.distance} {move.inDmg} {move.learned}");
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
                Debug.Log($"Learned {move.attackType} {move.side} {move.orientation} {move.distance} {move.inDmg} {move.learned}");
                break;
            }
        }
        val = -1f;
    }

    public override void UpdateState(HeroStateManager state, bool[] actions)
    {
        base.UpdateState(state, actions);

        if(val < 1f)
        {
            val += (Time.deltaTime * 0.5f);
            state.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_blend", val);
        }
        else
        {
            state.gameObject.transform.position = new Vector2(-11f, -3.1f);
            state.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sharedMaterial.SetFloat("_blend", 0f);
            state.gameObject.GetComponent<HeroNeuralNetwork>().Learn();
            state.SwitchState(state.IdleState);
            return;
        }
    }
    public override void OnTriggerEnter2D(Collider2D coll, HeroStateManager state, Animator animator)
    {
        base.OnTriggerEnter2D(coll, state, animator);
    }
}
