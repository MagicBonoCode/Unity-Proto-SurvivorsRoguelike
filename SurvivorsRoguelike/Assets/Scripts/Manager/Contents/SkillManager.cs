using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    public HashSet<BaseActiveSkill> Skills { get; private set; } = new HashSet<BaseActiveSkill>();

    public void AddActiveSkill(Define.ActiveSkillType skillType, Player player, Transform parent = null)
    {
        GameObject skillGameObject;
        switch (skillType)
        {
            case Define.ActiveSkillType.Bullet:
                skillGameObject = Managers.Resource.Instantiate("BulletSkill.prefab");
                skillGameObject.transform.position = Vector3.zero;

                BulletSkill bulletSkill = skillGameObject.GetComponent<BulletSkill>();
                bulletSkill.transform.SetParent(parent);
                bulletSkill.Init(player);
                Skills.Add(bulletSkill);
                break;

            case Define.ActiveSkillType.Sword:
                skillGameObject = Managers.Resource.Instantiate("SwordSkill.prefab");
                skillGameObject.transform.position = Vector3.zero;

                SwordSkill swordSkill = skillGameObject.GetComponent<SwordSkill>();
                swordSkill.transform.SetParent(parent);
                swordSkill.Init(player);
                Skills.Add(swordSkill);
                break;
        }
    }

    public void StopSkills()
    {
        foreach (var skill in Skills)
        {
            skill.StopAllCoroutines();
        }
    }

    public void Clear()
    {
        StopSkills();
        Skills.Clear();
    }
}
