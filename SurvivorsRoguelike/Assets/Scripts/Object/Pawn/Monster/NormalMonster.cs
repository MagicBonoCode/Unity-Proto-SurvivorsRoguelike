using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonster : BaseMonster
{
    private const float BODY_REMOVE_DELAY = 1.0f;

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
