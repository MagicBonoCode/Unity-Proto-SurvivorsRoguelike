using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    public int Damage { get; protected set; } = 1;
    private float _coolTime = 1.0f;

    public void ActivateSkill()
    {
        StartCoroutine(CoStartSkill());
    }

    private IEnumerator CoStartSkill()
    {
        while (true)
        {
            DoSkillJob();
            yield return new WaitForSeconds(_coolTime);
        }
    }

    protected abstract void DoSkillJob();

    protected virtual void GenerateProjectile(BasePawn owner, Vector3 startPos, Vector3 moveDir)
    {
        Projectile projectile = Managers.Object.Spawn<Projectile>(startPos);
        projectile.SetInfo(owner, moveDir, Damage);
    }
}
