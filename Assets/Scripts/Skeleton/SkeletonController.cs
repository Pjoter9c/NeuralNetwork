using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonController : MonoBehaviour
{

    [SerializeField] private int _maxHealth = 5;
    private int _health;
    [SerializeField] private Image _healtBar;

    [SerializeField] private GameObject _hero;

    private void Start()
    {

        _hero.GetComponent<HeroInfo>().SetEnemyAttackType(0);

        _health = _maxHealth;
        _healtBar.fillAmount = 1f;


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

}
