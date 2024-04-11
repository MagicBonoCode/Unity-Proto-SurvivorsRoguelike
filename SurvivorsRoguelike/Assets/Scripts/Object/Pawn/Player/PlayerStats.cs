using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Define.CharacterType _characterType = Define.CharacterType.Default;

    private PlayerStat[] _stats;
    private PlayerStat[] _bonusStats;
    private bool _init = false;

    private const int BONUS_STAT_LEVEL_INTERVAL = 10;
    private const float DEFAULT_SPEED = 6.0f;
    private const float DEFAULT_PROJECTILE_SPEED = 10.0f;
    private const float DEFAULT_LIFE_TIME = 3.0f;
    private const float DEFAULT_ATTACK_RANGE = 3.0f;
    private const float DEFAULT_COOL_TIME = 3.0f;
    private const float DEFAULT_MAGNET = 3.0f;

    public void Init()
    {
        if (!_init)
        {
            _stats = Managers.Data.PlayerInfoDictionary[_characterType].Stats;
            _bonusStats = Managers.Data.PlayerInfoDictionary[_characterType].BonusStats;
            _init = true;
        }
    }

    public int MaxHp
    {
        get
        {
            Init();
            return _stats[(int)Define.Stat.MaxHp].Value + (_bonusStats[(int)Define.Stat.MaxHp].Value * (Managers.Scene.GetCurrentScene<GameScene>().Level / BONUS_STAT_LEVEL_INTERVAL));
        }
    }

    public int Recovery
    {
        get
        {
            Init();
            return _stats[(int)Define.Stat.Recovery].Value + (_bonusStats[(int)Define.Stat.Recovery].Value * (Managers.Scene.GetCurrentScene<GameScene>().Level / BONUS_STAT_LEVEL_INTERVAL));
        }
    }

    public int Armor
    {
        get
        {
            Init();
            return _stats[(int)Define.Stat.Armor].Value + (_bonusStats[(int)Define.Stat.Armor].Value * (Managers.Scene.GetCurrentScene<GameScene>().Level / BONUS_STAT_LEVEL_INTERVAL));
        }
    }

    public float Speed
    {
        get
        {
            Init();
            return _stats[(int)Define.Stat.Speed].Value / 100.0f * DEFAULT_SPEED + (_bonusStats[(int)Define.Stat.Speed].Value * (Managers.Scene.GetCurrentScene<GameScene>().Level / BONUS_STAT_LEVEL_INTERVAL));
        }
    }

    public float Damage
    {
        get
        {
            Init();
            return _stats[(int)Define.Stat.Damage].Value / 100.0f + (_bonusStats[(int)Define.Stat.Damage].Value * (Managers.Scene.GetCurrentScene<GameScene>().Level / BONUS_STAT_LEVEL_INTERVAL));
        }
    }

    public float ProjectileSpeed
    {
        get
        {
            Init();
            return _stats[(int)Define.Stat.ProjectileSpeed].Value / 100.0f * DEFAULT_PROJECTILE_SPEED + (_bonusStats[(int)Define.Stat.ProjectileSpeed].Value * (Managers.Scene.GetCurrentScene<GameScene>().Level / BONUS_STAT_LEVEL_INTERVAL));
        }
    }

    public float LifeTime
    {
        get
        {
            Init();
            return _stats[(int)Define.Stat.LifeTime].Value / 100.0f * DEFAULT_LIFE_TIME + (_bonusStats[(int)Define.Stat.LifeTime].Value * (Managers.Scene.GetCurrentScene<GameScene>().Level / BONUS_STAT_LEVEL_INTERVAL));
        }
    }

    public float AttackRange
    {
        get
        {
            Init();
            return _stats[(int)Define.Stat.AttackRange].Value / 100.0f * DEFAULT_ATTACK_RANGE + (_bonusStats[(int)Define.Stat.AttackRange].Value * (Managers.Scene.GetCurrentScene<GameScene>().Level / BONUS_STAT_LEVEL_INTERVAL));
        }
    }

    public float CoolTime
    {
        get
        {
            Init();
            return _stats[(int)Define.Stat.CoolTime].Value / 100.0f * DEFAULT_COOL_TIME + (_bonusStats[(int)Define.Stat.CoolTime].Value * (Managers.Scene.GetCurrentScene<GameScene>().Level / BONUS_STAT_LEVEL_INTERVAL));
        }
    }

    public int ProjectileCount
    {
        get
        {
            Init();
            return _stats[(int)Define.Stat.ProjectileCount].Value + (_bonusStats[(int)Define.Stat.ProjectileCount].Value * (Managers.Scene.GetCurrentScene<GameScene>().Level / BONUS_STAT_LEVEL_INTERVAL));
        }
    }

    public float Magnet
    {
        get
        {
            Init();
            return _stats[(int)Define.Stat.Magnet].Value / 100.0f * DEFAULT_MAGNET + (_bonusStats[(int)Define.Stat.Magnet].Value * (Managers.Scene.GetCurrentScene<GameScene>().Level / BONUS_STAT_LEVEL_INTERVAL));
        }
    }
}
