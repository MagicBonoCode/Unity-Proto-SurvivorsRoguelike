using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Player : BasePawn
{
    [SerializeField] private Transform _indicator;
    public Transform Indicator { get { return _indicator; } }

    private int _experience;
    public int EXP { get { return _experience; } }

    private int _maxExperience;
    public int MaxEXP { get { return _maxExperience; } }

    private float _gemCollectDistance = 1.0f;
    private float _collisionDamageDelay = 0.5f;
    private float _collisionDamageCooldown = 0.0f;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        ObjectType = Define.ObjectType.Player;
        Level = 1;

        return true;
    }

    protected override void OnEnableObject()
    {
        base.OnEnableObject();

        PawnSpriteRenderer.sortingOrder = (int)Define.SpriteSortingOrder.Player;
        PawnState = Define.PawnState.Idle;
        
        // TODO : юс╫ц
        Managers.Skill.AddSkill<BulletSkill>(transform.position);
        Managers.Skill.AddSkill<SwordSkill>(transform.position);

        // Stats setting
        Damage = Managers.Data.PlayerStatsDictionary[Level].Damage;
        MaxHp = Managers.Data.PlayerStatsDictionary[Level].MaxHp;
        Speed = Managers.Data.PlayerStatsDictionary[Level].Speed;
        Hp = MaxHp;

        // Exp setting
        _experience = 0;
        _maxExperience = 100;
    }

    protected override void FadeAnimation()
    {
        switch (PawnState)
        {
            case Define.PawnState.Idle:
                PawnAnimator.CrossFade("Stand", 0.1f);
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

    protected override void UpdateController()
    {
        base.UpdateController();

        if (PawnState != Define.PawnState.Dead)
        {
            InputKey();
            CollectGem();

            // OnCollisionStay2D Damage timer
            _collisionDamageCooldown = Mathf.Clamp(_collisionDamageCooldown += Time.deltaTime, 0.0f, _collisionDamageDelay);
        }
    }

    protected override void FixedUpdateController()
    {
        base.FixedUpdateController();

        if (PawnState != Define.PawnState.Dead)
        {
            MovePlayer();
        }
    }

    protected override void LateUpdateController()
    {
        base.LateUpdateController();

        if (PawnState != Define.PawnState.Dead && MoveDir.x != 0)
        {
            PawnSpriteRenderer.flipX = MoveDir.x < 0;
        }
    }

    private void MovePlayer()
    {
        if (MoveDir == Vector2.zero)
        {
            PawnState = Define.PawnState.Idle;
            return;
        }

        Vector3 movement = MoveDir * Speed * Time.fixedDeltaTime;
        transform.position += movement;

        if (MoveDir != Vector2.zero)
        {
            _indicator.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-movement.x, movement.y) * 180 / Mathf.PI);
        }

        PawnRigidbody2D.velocity = Vector2.zero;
        PawnState = Define.PawnState.Moving;
    }

    private void InputKey()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        MoveDir = new Vector2(horizontalInput, verticalInput).normalized;
    }

    private void CollectGem()
    {
        float sqrCollectDist = _gemCollectDistance * _gemCollectDistance;

        var findGems = Managers.Grid.GatherObjects(transform.position, _gemCollectDistance);

        foreach (var gameObject in findGems)
        {
            Gem gem = gameObject.GetComponent<Gem>();

            Vector3 dir = gem.transform.position - transform.position;
            if (dir.sqrMagnitude <= sqrCollectDist)
            {
                Managers.Object.Despawn(gem);
                
                _experience++;
                Managers.Event.TriggerEvent("EvUpdateExp");
            }
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Monster")
        {
            return;
        }

        BaseMonster monster = collision.gameObject.GetComponent<BaseMonster>();
        if (monster == null && monster.PawnState == Define.PawnState.Dead)
        {
            return;
        }

        if (_collisionDamageCooldown >= _collisionDamageDelay)
        {
            OnDamaged(monster.gameObject, monster.Damage);
            _collisionDamageCooldown = 0.0f;
        }
    }

    public override void OnDamaged(GameObject attacker, int damage)
    {
        base.OnDamaged(attacker, damage);

        Managers.Event.TriggerEvent("OnPlayerHit");
    }

    protected override void OnDead()
    {
        base.OnDead();

        Managers.Skill.StopSkills();
        if(Managers.Scene.CurrentScene is GameScene gameScene)
        {
            gameScene.State = Define.GameSceneState.PlayerDead;
        }
    }
}
