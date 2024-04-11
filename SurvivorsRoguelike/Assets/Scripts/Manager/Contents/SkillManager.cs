using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    public HashSet<BaseSkill> Skills { get; private set; } = new HashSet<BaseSkill>();

    public T AddSkill<T>(Transform parent = null) where T : BaseSkill
    {
        Type type = typeof(T);

        if (type == typeof(BulletSkill))
        {
            GameObject gameObject = Managers.Resource.Instantiate("BulletSkill.prefab");
            gameObject.transform.position = Vector3.zero;

            BulletSkill bulletSkill = gameObject.GetComponent<BulletSkill>();
            bulletSkill.transform.SetParent(parent);
            bulletSkill.ActivateSkill();
            Skills.Add(bulletSkill);

            return bulletSkill as T;
        }
        else if (type == typeof(SwordSkill))
        {
            GameObject gameObject = Managers.Resource.Instantiate("SwordSkill.prefab");
            gameObject.transform.position = Vector3.zero;

            SwordSkill swordSkill = gameObject.GetComponent<SwordSkill>();
            swordSkill.transform.SetParent(parent);
            swordSkill.ActivateSkill();
            Skills.Add(swordSkill);

            return swordSkill as T;
        }

        return null;
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
