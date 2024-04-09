using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    private Define.GameSceneState _state;
    public Define.GameSceneState State
    {
        get { return _state; }
        set
        {
            if (_state == value)
            {
                return;
            }

            _state = value;
            switch (_state)
            {
                case Define.GameSceneState.Play:
                    StartCoroutine(CoPlayGameScene());
                    break;

                case Define.GameSceneState.Stop:
                    break;

                case Define.GameSceneState.PlayerDead:
                    StartCoroutine(CoPlayGameOverPopup());
                    break;
            }
        }
    }

    private float _gameTimer = 0.0f;
    public float GameTimer { get { return _gameTimer; } }

    private int _level = 1;
    private float _spawnTimer = 5.0f;
    private float _spawnInterval = 5.0f;
    private int _spawnCount = 20;
    private int _maxMonsterCount = 100;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.GameScene;

        // Stage info setting
        _level = 1;
        _spawnInterval = Managers.Data.StageInfoDictionary[_level].SpawnInterval;
        _spawnCount = Managers.Data.StageInfoDictionary[_level].SpawnCount;
        _maxMonsterCount = Managers.Data.StageInfoDictionary[_level].MaxMonsterCount;
        _gameTimer = 0.0f;
        _spawnTimer = _spawnInterval;

        Managers.Grid.SetGrid();
        Managers.Object.Spawn<Player>(Vector3.zero);
        Managers.UI.ShowSceneUI<UI_GameScene>();

        State = Define.GameSceneState.Play;
    }

    private IEnumerator CoPlayGameScene()
    {
        while (true)
        {
            if (State == Define.GameSceneState.Play)
            {
                _gameTimer = Mathf.Clamp(_gameTimer += Time.deltaTime, 0.0f, Mathf.Infinity);
                _spawnTimer = Mathf.Clamp(_spawnTimer += Time.deltaTime, 0.0f, _spawnInterval);
                if (_spawnTimer >= _spawnInterval)
                {
                    TrySpawn();
                    _spawnTimer = 0.0f;
                }
            }

            yield return null;
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

    private IEnumerator CoPlayGameOverPopup()
    {
        yield return new WaitForSeconds(1.0f); // Popup delay
        Managers.UI.ShowPopupUI<UI_GameOverPopup>();
    }

    public void ReplayGame()
    {
        Managers.Object.Clear();

        // Stage info setting
        _level = 1;
        _spawnInterval = Managers.Data.StageInfoDictionary[_level].SpawnInterval;
        _spawnCount = Managers.Data.StageInfoDictionary[_level].SpawnCount;
        _maxMonsterCount = Managers.Data.StageInfoDictionary[_level].MaxMonsterCount;
        _gameTimer = 0.0f;
        _spawnTimer = 0.0f;

        Managers.Object.Spawn<Player>(Vector3.zero);

        State = Define.GameSceneState.Play;

        Managers.Event.TriggerEvent("EvReplayGame");
    }

    public override void Clear()
    {
        base.Clear();
        StopAllCoroutines();
    }
}
