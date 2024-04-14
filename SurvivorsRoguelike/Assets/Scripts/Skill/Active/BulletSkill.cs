using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSkill : BaseActiveSkill
{
    protected override void DoSkillJob()
    {
        if (Player == null)
        {
            return;
        }

        Vector3 spawnPos = Player.transform.position;
        for (int i = 0; i < ProjectileCount; i++)
        {
            float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
            float horizontalInput = Mathf.Cos(angle);
            float verticalInput = Mathf.Sin(angle);
            Vector3 moveDir = new Vector3(horizontalInput, verticalInput, 0.0f).normalized;

            GenerateProjectile(Player, spawnPos, moveDir);
        }
    }
}
