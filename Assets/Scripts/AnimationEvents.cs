using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private Animator _animator;
    private SkeletonController _skeletonController;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _skeletonController = GetComponent<SkeletonController>();
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
}
