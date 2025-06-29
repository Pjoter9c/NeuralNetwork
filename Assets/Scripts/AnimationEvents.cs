using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private Animator _animator;
    private SkeletonController _skeletonController;

    [SerializeField] private GameObject _sword;
    private BoxCollider2D _swordCollider;
    [SerializeField] private GameObject _attack3;
    private BoxCollider2D _attack3Collider;
    private SpriteRenderer _attack3SpriteRenderer;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _skeletonController = GetComponent<SkeletonController>();

        _swordCollider = _sword.GetComponent<BoxCollider2D>();
        _attack3Collider = _attack3.GetComponent<BoxCollider2D>();
        _attack3SpriteRenderer = _attack3.GetComponent<SpriteRenderer>();
    }
    void EndAttack1Animation()
    {
        _animator.SetBool("isAttack1", false);
        _animator.SetBool("isIdle", true);
        _skeletonController.SetAttacking(false);
    }
    void EndAttack2Animation()
    {
        _animator.SetBool("isAttack2", false);
        _animator.SetBool("isIdle", true);
        _skeletonController.SetAttacking(false);
    }
    void EndAttack3Animation()
    {
        _animator.SetBool("isAttack3", false);
        _animator.SetBool("isIdle", true);
        _skeletonController.SetAttacking(false);
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
        _attack3SpriteRenderer.enabled = true;
    }
    void DisableAttack3Collider()
    {
        _attack3Collider.enabled = false;
        _attack3SpriteRenderer.enabled= false;
    }
}
