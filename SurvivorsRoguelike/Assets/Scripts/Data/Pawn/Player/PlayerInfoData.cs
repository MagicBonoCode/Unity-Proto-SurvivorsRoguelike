using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfoData", menuName = "Scriptable Object/Pawn/Player/PlayerInfoData", order = 0)]
public class PlayerInfoData : ScriptableObject
{
    public PlayerInfo[] dataArray;

    private void Awake()
    {
        if (dataArray == null)
        {
            dataArray = new PlayerInfo[0];
        }
    }

    public Dictionary<Define.CharacterType, PlayerInfo> MakeDictionary()
    {
        Dictionary<Define.CharacterType, PlayerInfo> dictionary = new Dictionary<Define.CharacterType, PlayerInfo>();
        foreach (PlayerInfo data in dataArray)
        {
            dictionary.Add(data.CharacterType, data);
        }

        return dictionary;
    }
}

[System.Serializable]
public class PlayerStat
{
    [SerializeField]
    private Define.Stat _stat;
    public Define.Stat Stat { get { return _stat; } set { _stat = value; } }

    [SerializeField]
    private int _value;
    public int Value { get { return _value; } set { _value = value; } }
}

[System.Serializable]
public class PlayerInfo
{
    [SerializeField]
    private Define.CharacterType _characterType;
    public Define.CharacterType CharacterType { get { return _characterType; } set { _characterType = value; } }

    [SerializeField]
    private PlayerStat[] _stats;
    public PlayerStat[] Stats { get { return _stats; } set { _stats = value; } }

    [SerializeField]
    private PlayerStat[] _bonusStats;
    public PlayerStat[] BonusStats { get { return _bonusStats; } set { _bonusStats = value; } }

    [SerializeField]
    private int _defaultSkillID;
    public int DefaultSkillID { get { return _defaultSkillID; } set { _defaultSkillID = value; } }
}
