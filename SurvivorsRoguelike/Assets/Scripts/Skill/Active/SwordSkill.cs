using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkill : BaseActiveSkill
{
    public override int Damage { get { return (int)(_activeSkillStatsDataArray[Level - 1].Damage * _player.DamageRatio); } }
    public override float ProjectileSpeed { get { return _activeSkillStatsDataArray[Level - 1].ProjectileSpeed * _player.ProjectileSpeedRatio; } }
    public override float LifeTime { get { return _activeSkillStatsDataArray[Level - 1].LifeTime * _player.LifeTimeRatio; } }
    public override float AttackRange { get { return _activeSkillStatsDataArray[Level - 1].AttackRange * _player.AttackRangeRatio; } }
    public override float CoolTime { get { return _activeSkillStatsDataArray[Level - 1].CoolTime * _player.CoolTimeReductionRatio; } }
    public override int ProjectileCount { get { return _activeSkillStatsDataArray[Level - 1].ProjectileCount + _player.AdditionalProjectileCount; } }
    public override Sprite Icon { get { return Managers.Data.ActiveSkillInfoDictionary[Define.ActiveSkillType.Bullet].Icon; } }
    public override string Name { get { return Managers.Data.ActiveSkillInfoDictionary[Define.ActiveSkillType.Bullet].Name; } }
    public override string[] Descriptions { get { return Managers.Data.ActiveSkillInfoDictionary[Define.ActiveSkillType.Bullet].Descriptions; } }

    private ActiveSkillStatsData[] _activeSkillStatsDataArray = new ActiveSkillStatsData[0];
    private Player _player;

    [SerializeField] private ParticleSystem _particle;

    public override void Init(Player player)
    {
        _player = player;
        _activeSkillStatsDataArray = Managers.Data.ActiveSkillInfoDictionary[Define.ActiveSkillType.Sword].ActiveSkillStatsDataArray;
        Level = 1;

        ActivateSkill();
    }

    protected override void DoSkillJob()
    {
        if (_player == null)
        {
            return;
        }

        Vector3 tempAngle = _player.Indicator.transform.eulerAngles;
        _particle.gameObject.transform.localEulerAngles = tempAngle;
        float radian = Mathf.Deg2Rad * tempAngle.z * -1.0f + 90.0f;
        var particleMain = _particle.main;
        particleMain.startRotation = radian;

        _particle.gameObject.transform.position = _player.transform.position;
        _particle.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_player == collision.gameObject)
        {
            return;
        }

        BaseMonster monster = collision.gameObject.GetComponent<BaseMonster>();
        if (monster != null)
        {
            monster.OnDamaged(_player.Indicator.gameObject, Util.RandomDamage(Damage));
        }
    }
}
