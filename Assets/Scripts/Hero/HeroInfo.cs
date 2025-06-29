using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInfo : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    private float _dist;

    // Input values to neural network
    private bool _side;     // 0 - left, 1 - right
    private bool _orientation; // 0 - behind, 1 - front
    private int _distance; // 0 - near, 1 - close, 2 - far
    private bool _inDmg; // Mozna pominac???
    private int _enemyAttackType;

    private void Update()
    {
        // calculate side
        Vector2 side = transform.position - _enemy.transform.position;
        _side = side.x >= 0;

        // calculate orientation
        _orientation = Mathf.Sign(side.x) != Mathf.Sign(_enemy.transform.right.x);

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

        //print("Side: " + _side + " Orientation: " + _orientation + " Distance: " + _distance + " AttackType: " + _enemyAttackType);
    }

    public void SetEnemyAttackType(int attack) => _enemyAttackType = attack;

    public bool GetSide() => _side;
    public bool GetOrientation() => _orientation;
    public int GetDistance() => _distance;
    public int GetEnemyAttackType() => _enemyAttackType;
}
