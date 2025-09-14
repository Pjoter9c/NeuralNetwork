using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStateManager : MonoBehaviour
{
    public HeroBaseState CurrentState;

    public HeroAttackState AttackState = new HeroAttackState();
    public HeroDashState DashState = new HeroDashState();
    public HeroIdleState IdleState = new HeroIdleState();
    public HeroJumpState JumpState = new HeroJumpState();
    public HeroRunState RunState = new HeroRunState();
    public HeroDeadState DeadState = new HeroDeadState();

    private Animator _animator;
    [SerializeField] public float speed;
    [SerializeField] public float dashSpeed;

    [HideInInspector] public bool IsGrounded;
    [HideInInspector] public bool Jumped;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private GameObject _sword;
    private BoxCollider2D _swordCollider;

    [SerializeField] private GameObject _skeleton;
    private SkeletonController _skeletonController;


    // what should do based on neural network output
    private bool[] _actions = new bool[7]; // stay, left, right, dodgeL, dodgeR, jump, attack

    public List<List<Data>> trainingSample;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        CurrentState = IdleState;
        CurrentState.EnterState(this, _animator);

        _swordCollider = _sword.GetComponent<BoxCollider2D>();

        trainingSample = gameObject.GetComponent<ReadData>().datas;
        
        _skeletonController = _skeleton.GetComponent<SkeletonController>();
    }

    private void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);

        // when skeleton dead
        if(_skeletonController.GetHealth() == 0)
        {
            _actions[0] = true; 
            _actions[1] = false; 
            _actions[2] = false; 
            _actions[3] = false; 
            _actions[4] = false; 
            _actions[5] = false; 
            _actions[6] = false; 
        }
        CurrentState.UpdateState(this, _actions);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        CurrentState.OnTriggerEnter2D(coll, this, _animator);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        CurrentState.OnTriggerStay2D(collision, this, _animator);
    }

    // convert neural network to actions (double[] -> bool[])
    public void SetActions(double[] action)
    {
        for (int i = 0; i < _actions.Length; i++)
        {
            _actions[i] = false;
        }
        for (int i = 0; i < _actions.Length; i++)
        {
            if (action[i] == 1)
            {
                _actions[i] = true;
                return;
            }
        }
        _actions[0] = true;
    }

    public void SwitchState(HeroBaseState state)
    {
        CurrentState = state;
        state.EnterState(this, _animator);
    }

    public float GetDashDirection()
    {
        float dir = _actions[3] ? -1f : 1f;
        return dir;
    }
    public bool[] GetActions() => _actions;

    /* --- Animation Events functions --- */
    private void EndDash()
    {
        CurrentState = IdleState;
        IdleState.EnterState(this, _animator);
    }

    private void MidAir()
    {
        _animator.SetBool("IsInAir", true);
        _animator.SetBool("IsIdle", false);
        _animator.SetBool("IsRunning", false);
        Jumped = true;
    }

    private void AttackToIdle()
    {
        if (IsGrounded)
        {
            CurrentState = IdleState;
            IdleState.EnterState(this, _animator);

            _animator.SetBool("IsIdle", true);
            _animator.SetBool("IsInAir", false);
            _animator.SetBool("IsRunning", false);
        }
    }

    private void DisbleSwordCollider()
    {
        _swordCollider.enabled = false;
    }
    private void EnableSwordCollider()
    {
        _swordCollider.enabled = true;
    }

}
