using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsData", menuName = "Scriptable Object/Pawn/Player/PlayerStatsData", order = 0)]
public class PlayerStatsData : ScriptableObject
{
    public PlayerStats[] dataArray;

    private void Awake()
    {
        if (dataArray == null)
        {
            dataArray = new PlayerStats[0];
        }
    }

    public Dictionary<int, PlayerStats> MakeDictionary()
    {
        Dictionary<int, PlayerStats> dictionary = new Dictionary<int, PlayerStats>();
        foreach (PlayerStats data in dataArray)
        {
            dictionary.Add(data.Level, data);
        }

        return dictionary;
    }
}

[System.Serializable]
public class PlayerStats
{
    [SerializeField]
    private int _level;
    public int Level { get { return _level; } set { _level = value; } }

    [SerializeField]
    private int _damage;
    public int Damage { get { return _damage; } set { _damage = value; } }

    [SerializeField]
    private int _maxHp;
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }

    [SerializeField]
    private float _speed;
    public float Speed { get { return _speed; } set { _speed = value; } }
}
