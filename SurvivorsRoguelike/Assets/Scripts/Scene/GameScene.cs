using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class GameScene : BaseScene
{
    private int _level = 1;
    private float _spawnInterval = 5.0f;
    private int _spawnCount = 20;
    private int _maxMonsterCount = 100;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.GameScene;
        _level = 1;

        // Stage Info Setting
        _spawnInterval = Managers.Data.StageInfoDictionary[_level].SpawnInterval;
        _spawnCount = Managers.Data.StageInfoDictionary[_level].SpawnCount;
        _maxMonsterCount = Managers.Data.StageInfoDictionary[_level].MaxMonsterCount;

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

        for (int i = 0; i < _spawnCount; i++)
        {
            Vector3 randPos = Util.GenerateMonsterSpawnPosition(Managers.Object.Player.transform.position, 10, 15);
            Managers.Object.Spawn<NormalMonster>(randPos);
        }
    }
}
