using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PassiveSkill : MonoBehaviour
{
    public int MaxLevel { get; private set; }
    public int Level { get; private set; }
    public int MaxHp { get { return _additionalPlayerStatsDataArray[Level - 1].MaxHp; } }
    public int Recovery { get { return _additionalPlayerStatsDataArray[Level - 1].Recovery; } }
    public int Armor { get { return _additionalPlayerStatsDataArray[Level - 1].Armor; } }
    public float Speed { get { return _additionalPlayerStatsDataArray[Level - 1].Speed; } }
    public float DamageRatio { get { return _additionalPlayerStatsDataArray[Level - 1].DamageRatio; } }
    public float ProjectileSpeedRatio { get { return _additionalPlayerStatsDataArray[Level - 1].ProjectileSpeedRatio; } }
    public float LifeTimeRatio { get { return _additionalPlayerStatsDataArray[Level - 1].LifeTimeRatio; } }
    public float AttackRangeRatio { get { return _additionalPlayerStatsDataArray[Level - 1].AttackRangeRatio; } }
    public float CoolTimeReductionRatio { get { return _additionalPlayerStatsDataArray[Level - 1].CoolTimeReductionRatio; } }
    public int AdditionalProjectileCount { get { return _additionalPlayerStatsDataArray[Level - 1].AdditionalProjectileCount; } }
    public float Magnet { get { return _additionalPlayerStatsDataArray[Level - 1].Magnet; } }
    public Sprite Icon { get { return Managers.Data.PassiveSkillInfoDictionary[_passiveSkillType].Icon; } }
    public string Name { get { return Managers.Data.PassiveSkillInfoDictionary[_passiveSkillType].Name; } }
    public string Description { get { return Managers.Data.PassiveSkillInfoDictionary[_passiveSkillType].Descriptions[Level - 1]; } }

    private Define.PassiveSkillType _passiveSkillType;
    private PlayerStatsData[] _additionalPlayerStatsDataArray = new PlayerStatsData[0];

    public void Init(Define.PassiveSkillType passiveSkillType)
    {
        _passiveSkillType = passiveSkillType;
        _additionalPlayerStatsDataArray = Managers.Data.PassiveSkillInfoDictionary[_passiveSkillType].AdditionalPlayerStatsDataArray;
        MaxLevel = Managers.Data.PassiveSkillInfoDictionary[_passiveSkillType].AdditionalPlayerStatsDataArray.Length;
        Level = 1;
    }

    public void PassiveSkillLevelUp()
    {
        if (Level < MaxLevel)
        {
            Level++;
        }
    }
}
