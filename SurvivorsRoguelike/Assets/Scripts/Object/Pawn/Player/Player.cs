using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Player : BasePawn
{
    [SerializeField] private ParticleSystem _bleedingParticle;
    [SerializeField] private Transform _indicator;
    public Transform Indicator { get { return _indicator; } }

    private PlayerStats _playerStat;
    private Coroutine _coCollisionStayCheck;

    private const float COLLISION_DAMAGE_DELAY = 0.5f;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        ObjectType = Define.ObjectType.Player;
        _playerStat = GetComponent<PlayerStats>();

        return true;
    }

    protected override void OnEnableObject()
    {
        base.OnEnableObject();

        Managers.Skill.AddSkill<SwordSkill>();

        PawnSpriteRenderer.sortingOrder = (int)Define.SpriteSortingOrder.Player;
        PawnState = Define.PawnState.Idle;
        Hp = _playerStat.MaxHp;
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

        Vector3 movement = MoveDir * _playerStat.Speed * Time.fixedDeltaTime;
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
        float sqrCollectDist = _playerStat.Magnet * _playerStat.Magnet;

        var findGems = Managers.Grid.GatherObjects(transform.position, _playerStat.Magnet);

        foreach (var gameObject in findGems)
        {
            Gem gem = gameObject.GetComponent<Gem>();

            Vector3 dir = gem.transform.position - transform.position;
            if (dir.sqrMagnitude <= sqrCollectDist)
            {
                Managers.Object.Despawn(gem);
                Managers.Scene.GetCurrentScene<GameScene>().Exp++;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
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

        if (_coCollisionStayCheck == null)
        {
            _coCollisionStayCheck = StartCoroutine(CoCollisionStayCheck(monster.gameObject, monster.Damage));

            if (!_bleedingParticle.isPlaying)
            {
                _bleedingParticle.Play();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_coCollisionStayCheck != null)
        {
            StopCoroutine(_coCollisionStayCheck);
            _coCollisionStayCheck = null;

            if (_bleedingParticle.isPlaying)
            {
                _bleedingParticle.Stop();
            }
        }
    }

    private IEnumerator CoCollisionStayCheck(GameObject attacker, int damage)
    {
        while (true)
        {
            OnDamaged(attacker, damage);
            yield return new WaitForSeconds(COLLISION_DAMAGE_DELAY);
        }
    }

    public override void OnDamaged(GameObject attacker, int damage)
    {
        base.OnDamaged(attacker, damage);
    }

    protected override void OnDead()
    {
        base.OnDead();

        if (_bleedingParticle.isPlaying)
        {
            _bleedingParticle.Stop();
        }

        Managers.Skill.StopSkills();
        Managers.Scene.GetCurrentScene<GameScene>().State = Define.GameSceneState.PlayerDead;
    }
}
