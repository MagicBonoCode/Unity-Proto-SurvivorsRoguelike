using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePawn : BaseObject
{
    public int Level { get; protected set; }
    public int Damage { get; protected set; }
    public int MaxHp { get; protected set; }
    public int Hp { get; protected set; }
    public float Speed { get; protected set; }
    public Vector2 MoveDir { get; protected set; }

    private Define.PawnState _pawnState = Define.PawnState.None;
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

    protected abstract void FadeAnimation();

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;
    private CircleCollider2D _circleCollider2D;

    protected Animator PawnAnimator { get { return _animator; } }
    protected SpriteRenderer PawnSpriteRenderer { get { return _spriteRenderer; } }
    protected Rigidbody2D PawnRigidbody2D { get { return _rigidbody2D; } }

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _circleCollider2D = GetComponent<CircleCollider2D>();

        return true;
    }

    protected override void OnEnableObject()
    {
        base.OnEnableObject();

        _rigidbody2D.simulated = true;
        _rigidbody2D.velocity = Vector2.zero;
        _circleCollider2D.enabled = true;
    }

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
        _rigidbody2D.simulated = false;
        _rigidbody2D.velocity = Vector2.zero;
        _spriteRenderer.sortingOrder = 6;
        _circleCollider2D.enabled = false;
    }
}
