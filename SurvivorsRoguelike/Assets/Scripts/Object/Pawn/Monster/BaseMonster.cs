using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class BaseMonster : BasePawn
{
    private float _pushForce = 5.0f;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        ObjectType = Define.ObjectType.Monster;
        Level = 1;

        return true;
    }

    protected override void OnEnableObject()
    {
        base.OnEnableObject();

        PawnSpriteRenderer.sortingOrder = (int)Define.SpriteSortingOrder.Monster;
        PawnState = Define.PawnState.Moving;

        // Stats setting
        Damage = Managers.Data.MonsterStatsDictionary[Level].Damage;
        MaxHp = Managers.Data.MonsterStatsDictionary[Level].MaxHp;
        Speed = Managers.Data.MonsterStatsDictionary[Level].Speed;

        Hp = MaxHp;
    }

    protected override void FadeAnimation()
    {
        switch (PawnState)
        {
            case Define.PawnState.Idle:
                break;
            case Define.PawnState.Moving:
                PawnAnimator.CrossFade("Run", 0.1f);
                break;
            case Define.PawnState.Dead:
                PawnAnimator.CrossFade("Dead", 0.1f);
                break;
            case Define.PawnState.Attacking:
                break;
        }
    }

    protected override void FixedUpdateController()
    {
        base.FixedUpdateController();

        switch (PawnState)
        {
            case Define.PawnState.Idle:
                break;
            case Define.PawnState.Moving:
                FixedUpdateMoving();
                break;
            case Define.PawnState.Dead:
                break;
            case Define.PawnState.Attacking:
                break;
        }
    }

    protected override void LateUpdateController()
    {
        base.LateUpdateController();

        switch (PawnState)
        {
            case Define.PawnState.Idle:
                break;
            case Define.PawnState.Moving:
                LateUpdateMoving();
                break;
            case Define.PawnState.Dead:
                break;
            case Define.PawnState.Attacking:
                break;
        }
    }

    private void FixedUpdateMoving()
    {
        GameObject player = Managers.Object.Player.gameObject;
        if (player == null || PawnAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }

        MoveDir = (player.transform.position - transform.position).normalized;
        Vector3 movement = MoveDir * Speed * Time.deltaTime;
        transform.position += movement;
    }

    private void LateUpdateMoving()
    {
        GameObject player = Managers.Object.Player.gameObject;
        MoveDir = (player.transform.position - transform.position).normalized;
        if (MoveDir.x != 0)
        {
            PawnSpriteRenderer.flipX = MoveDir.x < 0;
        }
    }

    public override void OnDamaged(GameObject attacker, int damage)
    {
        base.OnDamaged(attacker, damage);

        if (Hp <= 0)
        {
            return;
        }

        PawnAnimator.CrossFade("Hit", 0.1f);
        StartCoroutine(CoKnockBack(attacker));
    }

    private IEnumerator CoKnockBack(GameObject attacker)
    {
        yield return new WaitForFixedUpdate(); // Next one physics frame delay
        Vector2 pushDir = (transform.position - attacker.transform.position).normalized;
        PawnRigidbody2D.AddForce(pushDir * _pushForce, ForceMode2D.Impulse);
    }
}
