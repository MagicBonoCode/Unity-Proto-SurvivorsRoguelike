using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActiveSkillInfo", menuName = "Scriptable Object/Skill/ActiveSkillInfo", order = 0)]
public class ActiveSkillInfo : ScriptableObject
{
    [SerializeField]
    private ActiveSkillInfoData[] _dataArray;

    private void Awake()
    {
        if (_dataArray == null)
        {
            _dataArray = new ActiveSkillInfoData[0];
        }
    }

    public Dictionary<Define.ActiveSkillType, ActiveSkillInfoData> MakeDictionary()
    {
        Dictionary<Define.ActiveSkillType, ActiveSkillInfoData> dictionary = new Dictionary<Define.ActiveSkillType, ActiveSkillInfoData>();
        foreach (ActiveSkillInfoData data in _dataArray)
        {
            dictionary.Add(data.ActiveSkillType, data);
        }

        return dictionary;
    }
}

[Serializable]
public class ActiveSkillInfoData
{
    [SerializeField]
    private Define.ActiveSkillType _activeSkillType;
    public Define.ActiveSkillType ActiveSkillType { get { return _activeSkillType; } }

    [SerializeField]
    private ActiveSkillStatsData[] _activeSkillStatsDataArray;
    public ActiveSkillStatsData[] ActiveSkillStatsDataArray { get { return _activeSkillStatsDataArray; } }

    [SerializeField]
    private Sprite _icon;
    public Sprite Icon { get { return _icon; } }

    [SerializeField]
    private string _name;
    public string Name { get { return _name; } }

    [SerializeField]
    private string[] _descriptions;
    public string[] Descriptions { get { return _descriptions; } }
}

[Serializable]
public class ActiveSkillStatsData
{
    [SerializeField]
    private int _damage;
    public int Damage { get { return _damage; } }

    [SerializeField]
    private float _projectileSpeed;
    public float ProjectileSpeed { get { return _projectileSpeed; } }

    [SerializeField]
    private float _lifeTime;
    public float LifeTime { get { return _lifeTime; } }

    [SerializeField]
    private float _attackRange;
    public float AttackRange { get { return _attackRange; } }

    [SerializeField]
    private float _coolTime;
    public float CoolTime { get { return _coolTime; } }

    [SerializeField]
    private int _projectileCount;
    public int ProjectileCount { get { return _projectileCount; } }
}
