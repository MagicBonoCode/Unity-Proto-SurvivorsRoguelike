using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageInfoData", menuName = "Scriptable Object/Stage/StageInfoData", order = 0)]
public class StageInfoData : ScriptableObject
{
    public StageInfo[] dataArray;

    private void Awake()
    {
        if (dataArray == null)
        {
            dataArray = new StageInfo[0];
        }
    }

    public Dictionary<int, StageInfo> MakeDictionary()
    {
        Dictionary<int, StageInfo> dictionary = new Dictionary<int, StageInfo>();
        foreach (StageInfo data in dataArray)
        {
            dictionary.Add(data.Level, data);
        }

        return dictionary;
    }
}

[System.Serializable]
public class StageInfo
{
    [SerializeField]
    private int _level;
    public int Level { get { return _level; } set { _level = value; } }

    [SerializeField]
    private float _spawnInterval;
    public float SpawnInterval { get { return _spawnInterval; } set { _spawnInterval = value; } }

    [SerializeField]
    private int _spawnCount;
    public int SpawnCount { get { return _spawnCount; } set { _spawnCount = value; } }

    [SerializeField]
    private int _maxMonsterCount;
    public int MaxMonsterCount { get { return _maxMonsterCount; } set { _maxMonsterCount = value; } }
}
