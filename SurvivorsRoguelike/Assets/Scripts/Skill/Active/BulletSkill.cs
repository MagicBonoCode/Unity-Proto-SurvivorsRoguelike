using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSkill : BaseActiveSkill
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

    public override void Init(Player player)
    {
        _player = player;
        _activeSkillStatsDataArray = Managers.Data.ActiveSkillInfoDictionary[Define.ActiveSkillType.Bullet].ActiveSkillStatsDataArray;
        Level = 1;

        ActivateSkill();
    }

    protected override void DoSkillJob()
    {
        if (_player == null)
        {
            return;
        }

        Vector3 spawnPos = _player.transform.position;
        for (int i = 0; i < ProjectileCount; i++)
        {
            float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
            float horizontalInput = Mathf.Cos(angle);
            float verticalInput = Mathf.Sin(angle);
            Vector3 moveDir = new Vector3(horizontalInput, verticalInput, 0.0f).normalized;

            GenerateProjectile(_player, spawnPos, moveDir);
        }
    }
}
