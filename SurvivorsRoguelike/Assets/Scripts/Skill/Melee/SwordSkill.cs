using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkill : BaseSkill
{
    [SerializeField] private ParticleSystem _particle;

    protected override void DoSkillJob()
    {
        if (Managers.Object.Player == null)
        {
            return;
        }

        Vector3 tempAngle = Managers.Object.Player.Indicator.transform.eulerAngles;
        _particle.gameObject.transform.localEulerAngles = tempAngle;
        float radian = Mathf.Deg2Rad * tempAngle.z * -1.0f + 90.0f;
        var particleMain = _particle.main;
        particleMain.startRotation = radian;

        _particle.gameObject.transform.position = Managers.Object.Player.transform.position;
        _particle.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Managers.Object.Player == collision.gameObject ||
            collision.gameObject.tag == "MainCamera")
        {
            return;
        }

        BaseMonster monster = collision.gameObject.GetComponent<BaseMonster>();
        if (monster != null)
        {
            monster.OnDamaged(Managers.Object.Player.Indicator.gameObject, Damage);
        }
    }
}
