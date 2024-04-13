using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePawn : BaseObject
{
    public virtual int MaxHp { get; }
    public virtual int Recovery { get; }
    public virtual int Armor { get; }
    public virtual float Speed { get; }
    public int Hp { get; protected set; }
    public Vector2 MoveDir { get; protected set; }

    private Define.PawnState _pawnState = Define.PawnState.Default;
    public Define.PawnState PawnState
    {
        get { return _pawnState; }
        protected set
        {
            if (_pawnState == value)
            {
                return;
            }

            _pawnState = value;

            FadeAnimation();
        }
    }

    protected Animator PawnAnimator { get; private set; }
    protected SpriteRenderer PawnSpriteRenderer { get; private set; }
    protected Rigidbody2D PawnRigidbody2D { get; private set; }
    protected CircleCollider2D PawnCircleCollider2D { get; private set; }

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        PawnAnimator = GetComponent<Animator>();
        PawnSpriteRenderer = GetComponent<SpriteRenderer>();
        PawnRigidbody2D = GetComponent<Rigidbody2D>();
        PawnCircleCollider2D = GetComponent<CircleCollider2D>();

        return true;
    }

    protected override void OnEnableObject()
    {
        base.OnEnableObject();

        PawnRigidbody2D.simulated = true;
        PawnRigidbody2D.velocity = Vector2.zero;
        PawnCircleCollider2D.enabled = true;
    }
    
    protected abstract void FadeAnimation();

    public virtual void OnDamaged(GameObject attacker, int damage)
    {
        if (PawnState == Define.PawnState.Dead)
        {
            return;
        }

        Hp -= damage;

        if (Hp <= 0)
        {
            OnDead();
        }
    }

    protected virtual void OnDead()
    {
        PawnState = Define.PawnState.Dead;
        PawnRigidbody2D.simulated = false;
        PawnRigidbody2D.velocity = Vector2.zero;
        PawnSpriteRenderer.sortingOrder = 6;
        PawnCircleCollider2D.enabled = false;
    }
}
