using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveSkillInfo", menuName = "Scriptable Object/Skill/PassiveSkillInfo", order = 1)]
public class PassiveSkillInfo : ScriptableObject
{
    [SerializeField]
    private PassiveSkillInfoData[] _dataArray;

    private void Awake()
    {
        if (_dataArray == null)
        {
            _dataArray = new PassiveSkillInfoData[0];
        }
    }

    public Dictionary<Define.PassiveSkillType, PassiveSkillInfoData> MakeDictionary()
    {
        Dictionary<Define.PassiveSkillType, PassiveSkillInfoData> dictionary = new Dictionary<Define.PassiveSkillType, PassiveSkillInfoData>();
        foreach (PassiveSkillInfoData data in _dataArray)
        {
            dictionary.Add(data.PassiveSkillType, data);
        }

        return dictionary;
    }
}

[Serializable]
public class PassiveSkillInfoData
{
    [SerializeField]
    private Define.PassiveSkillType _passiveSkillType;
    public Define.PassiveSkillType PassiveSkillType { get { return _passiveSkillType; } }

    [SerializeField]
    private PlayerStatsData[] _additionalPlayerStatsDataArray;
    public PlayerStatsData[] AdditionalPlayerStatsDataArray { get { return _additionalPlayerStatsDataArray; } }

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
