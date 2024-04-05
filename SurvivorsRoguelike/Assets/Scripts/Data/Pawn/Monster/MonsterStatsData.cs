using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterStatsData", menuName = "Scriptable Object/Pawn/Monster/MonsterStatsData", order = 0)]
public class MonsterStatsData : ScriptableObject
{
    public MonsterStats[] dataArray;

    private void Awake()
    {
        if (dataArray == null)
        {
            dataArray = new MonsterStats[0];
        }
    }

    public Dictionary<int, MonsterStats> MakeDictionary()
    {
        Dictionary<int, MonsterStats> dictionary = new Dictionary<int, MonsterStats>();
        foreach (MonsterStats data in dataArray)
        {
            dictionary.Add(data.Level, data);
        }

        return dictionary;
    }
}

[System.Serializable]
public class MonsterStats
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
