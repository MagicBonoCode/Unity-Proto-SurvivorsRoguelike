using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfo", menuName = "Scriptable Object/Pawn/PlayerInfo", order = 0)]
public class PlayerInfo : ScriptableObject
{
    [SerializeField]
    private PlayerInfoData[] _dataArray;

    private void Awake()
    {
        if (_dataArray == null)
        {
            _dataArray = new PlayerInfoData[0];
        }
    }

    public Dictionary<Define.PlayerType, PlayerInfoData> MakeDictionary()
    {
        Dictionary<Define.PlayerType, PlayerInfoData> dictionary = new Dictionary<Define.PlayerType, PlayerInfoData>();
        foreach (PlayerInfoData data in _dataArray)
        {
            dictionary.Add(data.PlayerType, data);
        }

        return dictionary;
    }
}

[Serializable]
public class PlayerInfoData
{
    [SerializeField]
    private Define.PlayerType _playerType;
    public Define.PlayerType PlayerType { get { return _playerType; } }

    [SerializeField]
    private PlayerStatsData[] _playerStatsDataArray;
    public PlayerStatsData[] PlayerStatsDataArray { get { return _playerStatsDataArray; } }

    [SerializeField]
    private Define.ActiveSkillType _defaultActiveSkill;
    public Define.ActiveSkillType DefaultActiveSkill { get { return _defaultActiveSkill; } }

    [SerializeField]
    private Sprite _icon;
    public Sprite Icon { get { return _icon; } }

    [SerializeField]
    private string _name;
    public string Name { get { return _name; } }

    [SerializeField]
    private string _description;
    public string Description { get { return _description; } }
}

[Serializable]
public class PlayerStatsData
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
    private float _damageRatio;
    public float DamageRatio { get { return _damageRatio; } }

    [SerializeField]
    private float _projectileSpeedRatio;
    public float ProjectileSpeedRatio { get { return _projectileSpeedRatio; } }

    [SerializeField]
    private float _lifeTimeRatio;
    public float LifeTimeRatio { get { return _lifeTimeRatio; } }

    [SerializeField]
    private float _attackRangeRatio;
    public float AttackRangeRatio { get { return _attackRangeRatio; } }

    [SerializeField]
    private float _coolTimeReductionRatio;
    public float CoolTimeReductionRatio { get { return _coolTimeReductionRatio; } }

    [SerializeField]
    private int _additionalProjectileCount;
    public int AdditionalProjectileCount { get { return _additionalProjectileCount; } }

    [SerializeField]
    private float _magnet;
    public float Magnet { get { return _magnet; } }
}
