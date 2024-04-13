using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageInfo", menuName = "Scriptable Object/Stage/StageInfo", order = 0)]
public class StageInfo : ScriptableObject
{
    [SerializeField]
    private StageInfoData _data;
    public StageInfoData Data { get { return _data; } }
}

[Serializable]
public class StageInfoData
{
    [SerializeField]
    private int[] _nextLevelExperienceArray;
    public int[] NextLevelExperienceArray { get { return _nextLevelExperienceArray; } }
}
