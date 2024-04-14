using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    public Dictionary<Define.ActiveSkillType, BaseActiveSkill> ActiveSkills { get; private set; } = new Dictionary<Define.ActiveSkillType, BaseActiveSkill>();
    public Dictionary<Define.PassiveSkillType, PassiveSkill> PassiveSkills { get; private set; } = new Dictionary<Define.PassiveSkillType, PassiveSkill>();

    public void AddActiveSkill(Define.ActiveSkillType activeSkillType, Player player, Transform parent = null)
    {
        GameObject skillGameObject;
        if (ActiveSkills.ContainsKey(activeSkillType))
        {
            ActiveSkills[activeSkillType].ActiveSkillLevelUp();
            return;
        }

        switch (activeSkillType)
        {
            case Define.ActiveSkillType.Bullet:
                skillGameObject = Managers.Resource.Instantiate("BulletSkill.prefab");
                skillGameObject.transform.position = Vector3.zero;

                BulletSkill bulletSkill = skillGameObject.GetComponent<BulletSkill>();
                bulletSkill.transform.SetParent(parent);
                bulletSkill.Init(activeSkillType, player);
                ActiveSkills[activeSkillType] = bulletSkill;
                break;

            case Define.ActiveSkillType.Sword:
                skillGameObject = Managers.Resource.Instantiate("SwordSkill.prefab");
                skillGameObject.transform.position = Vector3.zero;

                SwordSkill swordSkill = skillGameObject.GetComponent<SwordSkill>();
                swordSkill.transform.SetParent(parent);
                swordSkill.Init(activeSkillType, player);
                ActiveSkills[activeSkillType] = swordSkill;
                break;
        }
    }

    public void AddPassiveSkill(Define.PassiveSkillType passiveSkillType)
    {
        if (PassiveSkills.ContainsKey(passiveSkillType))
        {
            PassiveSkills[passiveSkillType].PassiveSkillLevelUp();
            return;
        }

        switch (passiveSkillType)
        {
            case Define.PassiveSkillType.Heart:
                PassiveSkill heartPassive = new PassiveSkill();
                heartPassive.Init(passiveSkillType);
                PassiveSkills[passiveSkillType] = heartPassive;
                break;
        }
    }

    public void StopActiveSkills()
    {
        foreach (var activeSkill in ActiveSkills)
        {
            activeSkill.Value.StopAllCoroutines();
        }
    }

    public void Clear()
    {
        StopActiveSkills();
        ActiveSkills.Clear();
        PassiveSkills.Clear();
    }
}
