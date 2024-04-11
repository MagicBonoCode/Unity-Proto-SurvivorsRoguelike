using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    // Pawn
    public Dictionary<Define.CharacterType, PlayerInfo> PlayerInfoDictionary { get; private set; } = new Dictionary<Define.CharacterType, PlayerInfo>();
    public Dictionary<int, MonsterStats> MonsterStatsDictionary { get; private set; } = new Dictionary<int, MonsterStats>();
    
    // Stage
    public Dictionary<int, StageExperienceInfo> StageExperienceInfoDictionary { get; private set; } = new Dictionary<int, StageExperienceInfo>();

    public void Init()
    {
        // Pawn
        PlayerInfoDictionary = LoadData<PlayerInfoData>("PlayerInfoData.asset").MakeDictionary();
        MonsterStatsDictionary = LoadData<MonsterStatsData>("MonsterStatsData.asset").MakeDictionary();
        
        // Stage
        StageExperienceInfoDictionary = LoadData<StageExperienceInfoData>("StageExperienceInfoData.asset").MakeDictionary();
    }

    private T LoadData<T>(string key) where T : ScriptableObject
    {
        T scriptableObject = Managers.Resource.Load<T>($"{key}");
        return scriptableObject;
    }
}
