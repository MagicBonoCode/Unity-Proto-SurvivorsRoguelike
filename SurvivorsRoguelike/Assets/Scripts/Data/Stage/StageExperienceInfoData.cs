using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageExperienceInfoData", menuName = "Scriptable Object/Stage/StageExperienceInfoData", order = 0)]
public class StageExperienceInfoData : ScriptableObject
{
    public StageExperienceInfo[] dataArray;

    private void Awake()
    {
        if (dataArray == null)
        {
            dataArray = new StageExperienceInfo[0];
        }
    }

    public Dictionary<int, StageExperienceInfo> MakeDictionary()
    {
        Dictionary<int, StageExperienceInfo> dictionary = new Dictionary<int, StageExperienceInfo>();
        foreach (StageExperienceInfo data in dataArray)
        {
            dictionary.Add(data.Level, data);
        }

        return dictionary;
    }
}

[System.Serializable]
public class StageExperienceInfo
{
    [SerializeField]
    private int _level;
    public int Level { get { return _level; } set { _level = value; } }

    [SerializeField]
    private int _nextLevelExperience;
    public int NextLevelExperience { get { return _nextLevelExperience; } set { _nextLevelExperience = value; } }
}
