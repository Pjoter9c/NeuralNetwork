using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1f;
    private Animator _anitamor;
    private bool _attacking = false;
    private void Start()
    {
        _anitamor = GetComponent<Animator>();

        _anitamor.SetBool("isIdle", true);
        _anitamor.SetBool("isWalking", false);
        _anitamor.SetBool("isAttack1", false);
        _anitamor.SetBool("isAttack2", false);
        _anitamor.SetBool("isAttack3", false);
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
      
        if (!_attacking && horizontal != 0f)
        {
            transform.Translate(Vector2.left * (_speed * Time.deltaTime));

            float angle = 90f + 90f * Mathf.Sign(horizontal);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

        }
        

        bool walking = horizontal != 0f? true : false;
        if(!_attacking)
        {
            _anitamor.SetBool("isWalking", walking);
            _anitamor.SetBool("isIdle", !walking);
        }

        if (!_attacking && Input.GetKeyDown(KeyCode.Alpha1))
        {
            _attacking = true;
            _anitamor.SetBool("isAttack1", true);
            _anitamor.SetBool("isWalking", false);
            _anitamor.SetBool("isIdle", false);
        }

        if (!_attacking && Input.GetKeyDown(KeyCode.Alpha2))
        {
            _attacking = true;
            _anitamor.SetBool("isAttack2", true);
            _anitamor.SetBool("isWalking", false);
            _anitamor.SetBool("isIdle", false);
        }

        if (!_attacking && Input.GetKeyDown(KeyCode.Alpha3))
        {
            _attacking = true;
            _anitamor.SetBool("isAttack3", true);
            _anitamor.SetBool("isWalking", false);
            _anitamor.SetBool("isIdle", false);
        }
    }

    public void SetAttacking(bool attacking) => _attacking = attacking;
}
