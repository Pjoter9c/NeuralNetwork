using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInfo : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    private float _dist;

    // Input values to neural network
    private double _side;     // 0 - left, 1 - right
    private double _orientation; // 0 - behind, 1 - front
    private double _distance; // 0 - near, 1 - close, 2 - far
    private double _enemyAttackType;
    private double _inDmg; // TO DO: trigger kiedy npc bedzie w obszarze obrazen
    // TO DO: odleglosc do sciany -> po dodaniu scian nie bedzie uciekal w ich strone jesli bedzie za blisko

    private void Update()
    {
        // calculate side
        Vector2 side = transform.position - _enemy.transform.position;
        _side = side.x >= 0 ? 1 : 0;

        // calculate orientation
        _orientation = (Mathf.Sign(side.x) != Mathf.Sign(_enemy.transform.right.x)) ? 1 : 0;

        // calculate distance
        _dist = Mathf.Abs(side.x);
        // 0 - 0.65 - near
        // 0.65 - 4.5 - close
        // > 4.5 - far
        _distance = 0;
        if (_dist > 0.65f)
            _distance = 1;
        if (_dist > 4.5f)
            _distance = 2;
        //print("In: " + _enemyAttackType + _side + _orientation + _distance +  _inDmg);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            _inDmg = 1;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("HitBox"))
        {
            _inDmg = 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            _inDmg = 0;
        }
    }

    public void SetEnemyAttackType(int attack) => _enemyAttackType = attack;

    public double GetSide() => _side;
    public double GetOrientation() => _orientation;
    public double GetDistance() => _distance;
    public double GetEnemyAttackType() => _enemyAttackType;
    public double GetInDmg() => _inDmg;

    public double[] GetHeroInfo()
    {
        double[] output =
        {
            _enemyAttackType, _side, _orientation, _distance, _inDmg
        };

        return output;
    }
}
