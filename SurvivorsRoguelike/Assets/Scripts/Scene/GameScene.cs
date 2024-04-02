using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    private float _spawnInterval = 1.0f;
    private int _maxMonsterCount = 100;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.GameScene;

        Managers.Grid.SetGrid();
        Managers.Object.Spawn<Player>(Vector3.zero);

        StartCoroutine(CoUpdateSpawningPool());
    }

    private IEnumerator CoUpdateSpawningPool()
    {
        while (true)
        {
            TrySpawn();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void TrySpawn()
    {
        int monsterCount = Managers.Object.NormalMonsters.Count;
        if (monsterCount >= _maxMonsterCount)
        { 
            return;
        }

        Vector3 randPos = Util.GenerateMonsterSpawnPosition(Managers.Object.Player.transform.position, 10, 15);
        Managers.Object.Spawn<NormalMonster>(randPos);
    }
}
