using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    // Pawn
    public Dictionary<Define.PlayerType, PlayerInfoData> PlayerInfoDictionary { get; private set; } = new Dictionary<Define.PlayerType, PlayerInfoData>();
    public Dictionary<Define.MonsterType, MonsterInfoData> MonsterInfoDictionary { get; private set; } = new Dictionary<Define.MonsterType, MonsterInfoData>();

    // Skill
    public Dictionary<Define.ActiveSkillType, ActiveSkillInfoData> ActiveSkillInfoDictionary { get; private set; } = new Dictionary<Define.ActiveSkillType, ActiveSkillInfoData>();
    //public Dictionary<Define.PassiveSkillType, PassiveSkillInfoData> PassiveSkillInfoDictionary { get; private set; } = new Dictionary<Define.PassiveSkillType, PassiveSkillInfoData>();

    // Stage
    public StageInfoData StageInfo { get; private set; }

    public void Init()
    {
        // Pawn
        PlayerInfoDictionary = LoadData<PlayerInfo>("PlayerInfo.data").MakeDictionary();
        MonsterInfoDictionary = LoadData<MonsterInfo>("MonsterInfo.data").MakeDictionary();

        // Skill
        ActiveSkillInfoDictionary = LoadData<ActiveSkillInfo>("ActiveSkillInfo.data").MakeDictionary();
        //PassiveSkillInfoDictionary = LoadData<PassiveSkillInfo>("PassiveSkillInfo.data").MakeDictionary();

        // Stage
        StageInfo = LoadData<StageInfo>("StageInfo.data").Data;
    }

    private T LoadData<T>(string key) where T : ScriptableObject
    {
        T scriptableObject = Managers.Resource.Load<T>($"{key}");
        return scriptableObject;
    }
}
