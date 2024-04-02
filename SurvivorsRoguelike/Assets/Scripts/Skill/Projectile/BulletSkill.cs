using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletSkill : BaseSkill
{
    protected override void DoSkillJob()
    {
        if (Managers.Object.Player == null)
        {
            return;
        }

        Vector3 spawnPos = Managers.Object.Player.transform.position;
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
        float horizontalInput = Mathf.Cos(angle);
        float verticalInput = Mathf.Sin(angle);
        Vector3 moveDir = new Vector3(horizontalInput, verticalInput, 0.0f).normalized;

        GenerateProjectile(Managers.Object.Player, spawnPos, moveDir);
    }
}
