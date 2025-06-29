using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1f;
    private Animator _anitamor;
    private bool _attacking = false;

    [SerializeField] private int _maxHealth = 5;
    private int _health;
    [SerializeField] private Image _healtBar;

    [SerializeField] private GameObject _hero;

    private void Start()
    {
        _anitamor = GetComponent<Animator>();

        _anitamor.SetBool("isIdle", true);
        _anitamor.SetBool("isWalking", false);
        _anitamor.SetBool("isAttack1", false);
        _anitamor.SetBool("isAttack2", false);
        _anitamor.SetBool("isAttack3", false);

        _hero.GetComponent<HeroInfo>().SetEnemyAttackType(0);

        _health = _maxHealth;
        _healtBar.fillAmount = 1f;


    }

    private void Update()
    {
        //float horizontal = Input.GetAxis("Horizontal");
        float horizontal = 0f;

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

            _hero.GetComponent<HeroInfo>().SetEnemyAttackType(0);
        }

        if (!_attacking && Input.GetKeyDown(KeyCode.Alpha1))
        {
            _attacking = true;
            _anitamor.SetBool("isAttack1", true);
            _anitamor.SetBool("isWalking", false);
            _anitamor.SetBool("isIdle", false);

            _hero.GetComponent<HeroInfo>().SetEnemyAttackType(1);
        }

        if (!_attacking && Input.GetKeyDown(KeyCode.Alpha2))
        {
            _attacking = true;
            _anitamor.SetBool("isAttack2", true);
            _anitamor.SetBool("isWalking", false);
            _anitamor.SetBool("isIdle", false);

            _hero.GetComponent<HeroInfo>().SetEnemyAttackType(2);
        }

        if (!_attacking && Input.GetKeyDown(KeyCode.Alpha3))
        {
            _attacking = true;
            _anitamor.SetBool("isAttack3", true);
            _anitamor.SetBool("isWalking", false);
            _anitamor.SetBool("isIdle", false);

            _hero.GetComponent<HeroInfo>().SetEnemyAttackType(3);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.gameObject.CompareTag("HeroDmg"))
        {
            _health -= 1;
            _healtBar.fillAmount = (float)_health / _maxHealth;
            if (_health <= 0)
            {
                print("GAME OVER");
            }
        }
    }

    public void SetAttacking(bool attacking) => _attacking = attacking;
}
