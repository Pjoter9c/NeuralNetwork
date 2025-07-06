using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonStateManager : MonoBehaviour
{
    public SkeletonBaseState CurrentState;

    public SkeletonIdleState IdleState = new SkeletonIdleState();
    public SkeletonWalkState WalkState = new SkeletonWalkState();
    public SkeletonAttack1State Attack1State = new SkeletonAttack1State();
    public SkeletonAttack2State Attack2State = new SkeletonAttack2State();
    public SkeletonAttack3State Attack3State = new SkeletonAttack3State();


    Animator animator;
    [SerializeField] public float Speed;

    [SerializeField] private GameObject _hero;
    [HideInInspector] public HeroInfo HeroInfo;

    [SerializeField] public Canvas _canvasAttacks;
    [HideInInspector] public Image Attack1Image, Attack2Image, Attack3Image;

    [SerializeField] private GameObject _flames;
    [HideInInspector] public Material FlamesMaterial;


    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        HeroInfo = _hero.GetComponent<HeroInfo>();
        FlamesMaterial = _flames.GetComponent<Renderer>().material;

        Attack1Image = _canvasAttacks.transform.GetChild(0).GetComponent<Image>();
        Attack2Image = _canvasAttacks.transform.GetChild(1).GetComponent<Image>();
        Attack3Image = _canvasAttacks.transform.GetChild(2).GetComponent<Image>();

        CurrentState = IdleState;
        CurrentState.EnterState(this, animator);

    }

    private void Update()
    {
        CurrentState.UpdateState(this);
    }
    public void SwitchState(SkeletonBaseState state)
    {
        CurrentState = state;
        state.EnterState(this, animator);
    }
}
