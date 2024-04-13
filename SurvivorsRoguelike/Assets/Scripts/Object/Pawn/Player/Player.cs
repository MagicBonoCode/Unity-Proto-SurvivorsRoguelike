using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BasePawn
{
    public override int MaxHp { get { return _playerStatsDataArray[_level - 1].MaxHp; } }
    public override int Recovery { get { return _playerStatsDataArray[_level - 1].Recovery; } }
    public override int Armor { get { return _playerStatsDataArray[_level - 1].Armor; } }
    public override float Speed { get { return _playerStatsDataArray[_level - 1].Speed; } }

    public float DamageRatio { get { return _playerStatsDataArray[_level - 1].DamageRatio; } }
    public float ProjectileSpeedRatio { get { return _playerStatsDataArray[_level - 1].ProjectileSpeedRatio; } }
    public float LifeTimeRatio { get { return _playerStatsDataArray[_level - 1].Speed; } }
    public float AttackRangeRatio { get { return _playerStatsDataArray[_level - 1].AttackRangeRatio; } }
    public float CoolTimeReductionRatio { get { return _playerStatsDataArray[_level - 1].CoolTimeReductionRatio; } }
    public int AdditionalProjectileCount { get { return _playerStatsDataArray[_level - 1].AdditionalProjectileCount; } }
    public float Magnet { get { return _playerStatsDataArray[_level - 1].Magnet; } }

    public Define.ActiveSkillType DefaultActiveSkill { get; private set; }
    public Sprite Icon { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    [SerializeField] private ParticleSystem _bleedingParticle;
    [SerializeField] private Transform _indicator;
    public Transform Indicator { get { return _indicator; } }

    private PlayerStatsData[] _playerStatsDataArray = new PlayerStatsData[0];
    private int _level;
    private Coroutine _coCollisionStayCheck;

    private const float COLLISION_DAMAGE_DELAY = 0.5f;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        ObjectType = Define.ObjectType.Player;
        _playerStatsDataArray = Managers.Data.PlayerInfoDictionary[Define.PlayerType.TypeA].PlayerStatsDataArray;
        _level = 1;
        DefaultActiveSkill = Managers.Data.PlayerInfoDictionary[Define.PlayerType.TypeA].DefaultActiveSkill;
        Icon = Managers.Data.PlayerInfoDictionary[Define.PlayerType.TypeA].Icon;
        Name = Managers.Data.PlayerInfoDictionary[Define.PlayerType.TypeA].Name;
        Description = Managers.Data.PlayerInfoDictionary[Define.PlayerType.TypeA].Description;

        Managers.Event.RemoveEvent("EvUpdateSceneLevel", () => { _level = Managers.Scene.GetCurrentScene<GameScene>().Level; });
        Managers.Event.AddEvent("EvUpdateSceneLevel", () => { _level = Managers.Scene.GetCurrentScene<GameScene>().Level; });

        return true;
    }

    protected override void OnEnableObject()
    {
        base.OnEnableObject();

        PawnSpriteRenderer.sortingOrder = (int)Define.SpriteSortingOrder.Player;
        PawnState = Define.PawnState.Idle;
        Hp = MaxHp;

        // TODO: Skill Add
        Managers.Skill.AddActiveSkill(DefaultActiveSkill, this);
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
        float sqrCollectDist = Magnet * Magnet;

        var findGems = Managers.Grid.GatherObjects(transform.position, Magnet);

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
            _bleedingParticle.Play();
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

        _bleedingParticle.Stop();
        Managers.Skill.StopSkills();
        Managers.Scene.GetCurrentScene<GameScene>().State = Define.GameSceneState.PlayerDead;
    }
}
