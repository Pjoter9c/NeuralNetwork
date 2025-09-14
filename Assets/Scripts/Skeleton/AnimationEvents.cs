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

    [SerializeField] private GameObject[] _hitBoxes;

    [SerializeField] private GameObject _hero;
    private HeroInfo _heroInfo;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _skeletonStateManager = GetComponent<SkeletonStateManager>();

        _swordCollider = _sword.GetComponent<BoxCollider2D>();
        _attack3Collider = _attack3.GetComponent<BoxCollider2D>();

        _heroInfo = _hero.GetComponent<HeroInfo>();
    }

    void SendAttack1Info()
    {
        _heroInfo.SetEnemyAttackType(1);
        _hitBoxes[0].GetComponent<BoxCollider2D>().enabled = true;
    }
    void SendAttack2Info()
    {
        _heroInfo.SetEnemyAttackType(2);
        _hitBoxes[1].GetComponent<BoxCollider2D>().enabled = true;
    }
    void SendAttack3Info()
    {
        _heroInfo.SetEnemyAttackType(3);
        _hitBoxes[2].GetComponent<BoxCollider2D>().enabled = true;
    }
    void EndAttack()
    {
        if(Input.GetAxis("Horizontal") == 0)
            _skeletonStateManager.SwitchState(_skeletonStateManager.IdleState);
        else
            _skeletonStateManager.SwitchState(_skeletonStateManager.WalkState);
    }
    void EnableSwordCollider()
    {
        _swordCollider.enabled = true;
    }
    void DisableSwordCollider()
    {
        _swordCollider.enabled = false;
        foreach (var col in _hitBoxes)
        {
            col.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void EnableAttack3Collider()
    {
        _attack3Collider.enabled = true;
    }
    void DisableAttack3Collider()
    {
        _attack3Collider.enabled = false;
        _skeletonStateManager.EnableFlames = false;
        _attack3.GetComponent<Renderer>().material.SetFloat("_Fade", 0f);
        _hitBoxes[2].GetComponent<BoxCollider2D>().enabled = false;
    }

    void GameOverScreen()
    {
        _skeletonStateManager._killsTextMesh.text = "Kills: " + _heroInfo.Kills.ToString();
        _skeletonStateManager._gameOverCanvas.enabled = true;
    }
}
