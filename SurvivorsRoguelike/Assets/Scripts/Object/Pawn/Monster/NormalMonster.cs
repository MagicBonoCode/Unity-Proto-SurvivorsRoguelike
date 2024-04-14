using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonster : BaseMonster
{
    public override int MaxHp { get { return _monsterStatsDataArray[_level - 1].MaxHp; } }
    public override int Recovery { get { return _monsterStatsDataArray[_level - 1].Recovery; } }
    public override int Armor { get { return _monsterStatsDataArray[_level - 1].Armor; } }
    public override float Speed { get { return _monsterStatsDataArray[_level - 1].Speed; } }
    public override int Damage { get { return _monsterStatsDataArray[_level - 1].Damage; } }
    public override float ProjectileSpeed { get { return _monsterStatsDataArray[_level - 1].ProjectileSpeed; } }

    private MonsterStatsData[] _monsterStatsDataArray = new MonsterStatsData[0];
    private int _level;

    private const float BODY_REMOVE_DELAY = 1.0f;

    protected override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        _monsterStatsDataArray = Managers.Data.MonsterInfoDictionary[Define.MonsterType.TypeA].MonsterStatsDataArray;
        _level = 1;

        return true;
    }

    protected override void OnEnableObject()
    {
        base.OnEnableObject();
        _level = 1 + Managers.Scene.GetCurrentScene<GameScene>().Level / 10;
        Hp = MaxHp;
    }

    protected override void OnDead()
    {
        base.OnDead();

        Managers.Object.Spawn<Gem>(transform.position);
        StartCoroutine(CoRemoveBody());
    }

    private IEnumerator CoRemoveBody()
    {
        yield return new WaitForSeconds(BODY_REMOVE_DELAY);

        StopAllCoroutines();

        Managers.Object.Despawn(this);
    }
}
