using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseActiveSkill : MonoBehaviour
{
    public int Level { get; protected set; }
    public virtual int Damage { get; }
    public virtual float ProjectileSpeed { get; }
    public virtual float LifeTime { get; }
    public virtual float AttackRange { get; }
    public virtual float CoolTime { get; }
    public virtual int ProjectileCount { get; }
    public virtual Sprite Icon { get; }
    public virtual string Name { get; }
    public virtual string[] Descriptions { get; }

    public abstract void Init(Player player);

    protected void ActivateSkill()
    {
        StartCoroutine(CoStartSkill());
    }

    private IEnumerator CoStartSkill()
    {
        while (true)
        {
            DoSkillJob();
            yield return new WaitForSeconds(CoolTime);
        }
    }

    protected abstract void DoSkillJob();

    protected virtual void GenerateProjectile(Player owner, Vector3 startPos, Vector3 moveDir)
    {
        Projectile projectile = Managers.Object.Spawn<Projectile>(startPos);
        int damage = Damage;
        float speed = ProjectileSpeed;
        float lifeTime = LifeTime;
        projectile.SetInfo(owner, moveDir, damage, speed, lifeTime);
    }
}
