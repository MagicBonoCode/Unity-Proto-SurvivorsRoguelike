using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    // Pawn
    public Dictionary<int, PlayerStats> PlayerStatsDictionary { get; private set; } = new Dictionary<int, PlayerStats>();
    public Dictionary<int, MonsterStats> MonsterStatsDictionary { get; private set; } = new Dictionary<int, MonsterStats>();
    
    // Stage
    public Dictionary<int, StageInfo> StageInfoDictionary { get; private set; } = new Dictionary<int, StageInfo>();

    public void Init()
    {
        // Pawn
        PlayerStatsDictionary = LoadData<PlayerStatsData>("PlayerStatsData.asset").MakeDictionary();
        MonsterStatsDictionary = LoadData<MonsterStatsData>("MonsterStatsData.asset").MakeDictionary();
        
        // Stage
        StageInfoDictionary = LoadData<StageInfoData>("StageInfoData.asset").MakeDictionary();
    }

    private T LoadData<T>(string key) where T : ScriptableObject
    {
        T scriptableObject = Managers.Resource.Load<T>($"{key}");
        return scriptableObject;
    }
}
