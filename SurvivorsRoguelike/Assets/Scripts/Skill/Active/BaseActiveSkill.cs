using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public abstract class BaseActiveSkill : MonoBehaviour
{
    protected Player Player { get; private set; }
    public int MaxLevel { get; private set; }
    public int Level { get; private set; }
    public int Damage { get { return (int)(_activeSkillStatsDataArray[Level - 1].Damage * Player.DamageRatio); } }
    public float ProjectileSpeed { get { return _activeSkillStatsDataArray[Level - 1].ProjectileSpeed * Player.ProjectileSpeedRatio; } }
    public float LifeTime { get { return _activeSkillStatsDataArray[Level - 1].LifeTime * Player.LifeTimeRatio; } }
    public float AttackRange { get { return _activeSkillStatsDataArray[Level - 1].AttackRange * Player.AttackRangeRatio; } }
    public float CoolTime { get { return _activeSkillStatsDataArray[Level - 1].CoolTime * Player.CoolTimeReductionRatio; } }
    public int ProjectileCount { get { return _activeSkillStatsDataArray[Level - 1].ProjectileCount + Player.AdditionalProjectileCount; } }
    public Sprite Icon { get { return Managers.Data.ActiveSkillInfoDictionary[_activeSkillType].Icon; } }
    public string Name { get { return Managers.Data.ActiveSkillInfoDictionary[_activeSkillType].Name; } }
    public string Description { get { return Managers.Data.ActiveSkillInfoDictionary[_activeSkillType].Descriptions[Level - 1]; } }

    private Define.ActiveSkillType _activeSkillType;
    private ActiveSkillStatsData[] _activeSkillStatsDataArray = new ActiveSkillStatsData[0];

    public void Init(Define.ActiveSkillType activeSkillType, Player player)
    {
        _activeSkillType = activeSkillType;
        _activeSkillStatsDataArray = Managers.Data.ActiveSkillInfoDictionary[_activeSkillType].ActiveSkillStatsDataArray;
        Player = player;
        MaxLevel = Managers.Data.ActiveSkillInfoDictionary[_activeSkillType].ActiveSkillStatsDataArray.Length;
        Level = 1;

        ActivateSkill();
    }

    public void ActiveSkillLevelUp()
    {
        if (Level < MaxLevel)
        {
            Level++;
        }
    }

    private void ActivateSkill()
    {
        StartCoroutine(CoStartSkill());
    }

    private IEnumerator CoStartSkill()
    {
        while (true)
        {
            DoSkillJob();
            yield return new WaitForSeconds(CoolTime);
        }
    }

    protected abstract void DoSkillJob();

    protected virtual void GenerateProjectile(Player owner, Vector3 startPos, Vector3 moveDir)
    {
        Projectile projectile = Managers.Object.Spawn<Projectile>(startPos);
        projectile.SetInfo(owner, moveDir, Util.RandomDamage(Damage), ProjectileSpeed, LifeTime);
    }
}
