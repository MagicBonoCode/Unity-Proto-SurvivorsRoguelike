using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseObject
{
    private BasePawn _owner;
    private Vector3 _moveDir;
    private int _damage;
    private float _speed;
    private float _lifeTime;
    private float _maxLifeTime;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        ObjectType = Define.ObjectType.Projectile;

        return true;
    }

    public void SetInfo(BasePawn owner, Vector3 moveDir, int damage)
    {
        _owner = owner;
        _moveDir = moveDir;
        _damage = damage;
        _speed = 8.0f;
        _lifeTime = 0.0f;
        _maxLifeTime = 10.0f;
        
        transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-_moveDir.x, _moveDir.y) * 180 / Mathf.PI);
    }

    protected override void FixedUpdateController()
    {
        base.FixedUpdateController();

        transform.position += _moveDir * _speed * Time.fixedDeltaTime;

        _lifeTime = Mathf.Clamp(_lifeTime + Time.deltaTime, 0.0f, _maxLifeTime);
        if (_lifeTime >= _maxLifeTime)
        {
            Managers.Object.Despawn(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Area" || _owner.gameObject == collision.gameObject)
        {
            return;
        }
        
        BasePawn pawn = collision.GetComponent<BasePawn>();
        if(pawn == null)
        {
            return;
        }

        Type ownerType = _owner.GetType();
        if (ownerType == typeof(Player))
        {
            pawn.OnDamaged(gameObject, _damage);
        }
        else if (ownerType == typeof(BaseMonster) && pawn.GetType() == typeof(Player))
        {
            pawn.OnDamaged(gameObject, _damage);
        }

        Managers.Object.Despawn(this);
    }
}
