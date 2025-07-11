using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private Animator _animator;

    private SkeletonStateManager _skeletonStateManager;

    [SerializeField] private GameObject _sword;
    private BoxCollider2D _swordCollider;
    [SerializeField] private GameObject _attack3;
    private BoxCollider2D _attack3Collider;
    private SpriteRenderer _attack3SpriteRenderer;

    [SerializeField] private GameObject _hero;
    private HeroInfo _heroInfo;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _skeletonStateManager = GetComponent<SkeletonStateManager>();

        _swordCollider = _sword.GetComponent<BoxCollider2D>();
        _attack3Collider = _attack3.GetComponent<BoxCollider2D>();
        _attack3SpriteRenderer = _attack3.GetComponent<SpriteRenderer>();

        _heroInfo = _hero.GetComponent<HeroInfo>();
    }

    void SendAttack1Info()
    {
        _heroInfo.SetEnemyAttackType(1);
    }
    void SendAttack2Info()
    {
        _heroInfo.SetEnemyAttackType(2);
    }
    void SendAttack3Info()
    {
        _heroInfo.SetEnemyAttackType(3);
    }
    void EndAttack()
    {
        if(Input.GetAxis("Horizontal") == 0)
            _skeletonStateManager.SwitchState(_skeletonStateManager.IdleState);
        else
            _skeletonStateManager.SwitchState(_skeletonStateManager.WalkState);
    }
    void EndAttack2Animation()
    {
        _animator.SetBool("IsIdle", true);
        _animator.SetBool("IsWalking", false);
    }
    void EndAttack3Animation()
    {
        _animator.SetBool("IsIdle", true);
        _animator.SetBool("IsWalking", false);
    }
    void EnableSwordCollider()
    {
        _swordCollider.enabled = true;
    }
    void DisableSwordCollider()
    {
        _swordCollider.enabled = false;
    }

    void EnableAttack3Collider()
    {
        _attack3Collider.enabled = true;
        //_attack3SpriteRenderer.enabled = true;
    }
    void DisableAttack3Collider()
    {
        _attack3Collider.enabled = false;
        _attack3.GetComponent<Renderer>().material.SetFloat("_Fade", 0f);
        //_attack3SpriteRenderer.enabled= false;
    }
}
