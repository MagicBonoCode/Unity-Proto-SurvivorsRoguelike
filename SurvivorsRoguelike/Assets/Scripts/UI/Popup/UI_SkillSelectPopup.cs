using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_SkillSelectPopup : UI_BasePopup
{
    private enum GameObjects
    {
        Panel_ActiveSkills,
        Panel_PassiveSkills,
        Panel_Select,
    }

    private HashSet<Define.ActiveSkillType> _pickedActiveSkills = new HashSet<Define.ActiveSkillType>();
    private HashSet<Define.PassiveSkillType> _pickedPassiveSkills = new HashSet<Define.PassiveSkillType>();

    private GameObject _selectPanel;

    private const int PICK_COUNT = 3;

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        _selectPanel = GetObject((int)GameObjects.Panel_Select);

        PickSkills();
        SpawnSlot();
    }

    private void PickSkills()
    {
        while (_pickedActiveSkills.Count + _pickedPassiveSkills.Count < PICK_COUNT)
        {
            bool isActiveSkill = Random.Range(0, 2) == 0 ? true : false;
            if (isActiveSkill)
            {
                int index = Random.Range((int)Define.ActiveSkillType.Default + 1, (int)Define.ActiveSkillType.MaxCount);
                if (!_pickedActiveSkills.Contains((Define.ActiveSkillType)index))
                {
                    _pickedActiveSkills.Add((Define.ActiveSkillType)index);
                }
            }
            else
            {
                int index = Random.Range((int)Define.PassiveSkillType.Default + 1, (int)Define.PassiveSkillType.MaxCount);
                if (!_pickedPassiveSkills.Contains((Define.PassiveSkillType)index))
                {
                    _pickedPassiveSkills.Add((Define.PassiveSkillType)index);
                }
            }
        }
    }

    private void SpawnSlot()
    {
        foreach (Transform child in _selectPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }

        foreach (Define.ActiveSkillType activeSkillType in _pickedActiveSkills)
        {
            GameObject gameObject = Managers.Resource.Instantiate("UI_SkillSelectSlot", _selectPanel.transform);
            UI_SkillSelectSlot slot = gameObject.GetComponent<UI_SkillSelectSlot>();
            slot.SetSkillSelectSlot(activeSkillType);
        }

        foreach (Define.PassiveSkillType passiveSkillType in _pickedPassiveSkills)
        {
            GameObject gameObject = Managers.Resource.Instantiate("UI_SkillSelectSlot", _selectPanel.transform);
            UI_SkillSelectSlot slot = gameObject.GetComponent<UI_SkillSelectSlot>();
            slot.SetSkillSelectSlot(passiveSkillType);
        }
    }
}
