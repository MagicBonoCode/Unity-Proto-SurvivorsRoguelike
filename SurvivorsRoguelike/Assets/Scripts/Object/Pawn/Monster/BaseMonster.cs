using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMonster : BasePawn
{
    public virtual int Damage { get; }
    public virtual float ProjectileSpeed { get; }

    private const float PUSH_FORCE = 5.0f;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        ObjectType = Define.ObjectType.Monster;

        return true;
    }

    protected override void OnEnableObject()
    {
        base.OnEnableObject();

        PawnSpriteRenderer.sortingOrder = (int)Define.SpriteSortingOrder.Monster;
        PawnState = Define.PawnState.Moving;
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
        PawnRigidbody2D.AddForce(pushDir * PUSH_FORCE, ForceMode2D.Impulse);
    }
}
