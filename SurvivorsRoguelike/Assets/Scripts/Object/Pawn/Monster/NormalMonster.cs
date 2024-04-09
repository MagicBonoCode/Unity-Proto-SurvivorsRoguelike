using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonster : BaseMonster
{
    private float _bodyRemovalDelay = 1.0f;

    protected override void OnDead()
    {
        base.OnDead();

        Managers.Object.Spawn<Gem>(transform.position);
        StartCoroutine(CoRemoveBody());
    }

    private IEnumerator CoRemoveBody()
    {
        yield return new WaitForSeconds(_bodyRemovalDelay);

        StopAllCoroutines();

        Managers.Object.Despawn(this);
    }
}
