using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterInfo", menuName = "Scriptable Object/Pawn/MonsterInfo", order = 1)]
public class MonsterInfo : ScriptableObject
{
    [SerializeField]
    private MonsterInfoData[] _dataArray;

    private void Awake()
    {
        if (_dataArray == null)
        {
            _dataArray = new MonsterInfoData[0];
        }
    }

    public Dictionary<Define.MonsterType, MonsterInfoData> MakeDictionary()
    {
        Dictionary<Define.MonsterType, MonsterInfoData> dictionary = new Dictionary<Define.MonsterType, MonsterInfoData>();
        foreach (MonsterInfoData data in _dataArray)
        {
            dictionary.Add(data.MonsterType, data);
        }

        return dictionary;
    }
}

[Serializable]
public class MonsterInfoData
{
    [SerializeField]
    private Define.MonsterType _MonsterType;
    public Define.MonsterType MonsterType { get { return _MonsterType; } }

    [SerializeField]
    private MonsterStatsData[] _monsterStatsDataArray;
    public MonsterStatsData[] MonsterStatsDataArray { get { return _monsterStatsDataArray; } }
}

[Serializable]
public class MonsterStatsData
{
    [SerializeField]
    private int _maxHp;
    public int MaxHp { get { return _maxHp; } }

    [SerializeField]
    private int _recovery;
    public int Recovery { get { return _recovery; } }

    [SerializeField]
    private int _armor;
    public int Armor { get { return _armor; } }

    [SerializeField]
    private float _speed;
    public float Speed { get { return _speed; } }

    [SerializeField]
    private int _damage;
    public int Damage { get { return _damage; } }

    [SerializeField]
    private float _projectileSpeed;
    public float ProjectileSpeed { get { return _projectileSpeed; } }
}
