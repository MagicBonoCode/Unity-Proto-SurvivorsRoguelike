using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class GameScene : BaseScene
{
    private Define.GameSceneState _state = Define.GameSceneState.Default;
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
                    if (_coPlayGameScene == null)
                    {
                        _coPlayGameScene = StartCoroutine(CoPlayGameScene());
                    }
                    break;

                case Define.GameSceneState.Stop:
                    if (_coPlayGameScene != null)
                    {
                        StopCoroutine(_coPlayGameScene);
                        _coPlayGameScene = null;
                    }
                    break;

                case Define.GameSceneState.PlayerDead:
                    if (_coPlayGameScene != null)
                    {
                        StopCoroutine(_coPlayGameScene);
                        _coPlayGameScene = null;
                    }
                    StartCoroutine(CoPlayGameOverPopup());
                    break;
            }
        }
    }

    public float GameTimer { get; private set; }
    public int Level { get; private set; }
    public int MaxExp { get; private set; }

    private int _experiecne = 0;
    public int Exp { 
        get 
        { 
            return _experiecne; 
        }
        set
        {
            _experiecne = value;
            if (_experiecne >= MaxExp)
            { 
                Level = CalculateLevel();
                Managers.Event.TriggerEvent("EvUpdateStageLevel");
            }
            Managers.Event.TriggerEvent("EvUpdateExp");
        }
    }

    private Coroutine _coPlayGameScene;

    // TODO: Move to Data
    private float _spawnTimer = 5.0f;
    private float _spawnInterval = 5.0f;
    private int _spawnCount = 20;
    private int _maxMonsterCount = 100;

    protected override void Init()
    {
        base.Init();
        Managers.Scene.SetCurrentScene(this);
        Managers.Grid.SetGrid();
        Managers.UI.ShowSceneUI<UI_GameScene>();
        InitializeGameSettings();
    }

    private void InitializeGameSettings()
    {
        Level = 1;
        GameTimer = 0.0f;
        MaxExp = Managers.Data.StageExperienceInfoDictionary[Level].NextLevelExperience;
        _experiecne = 0;
        _spawnTimer = _spawnInterval;
        Managers.Object.Spawn<Player>(Vector3.zero);
        State = Define.GameSceneState.Play;

        Managers.Event.TriggerEvent("EvInitializeGameSettings");
    }

    private int CalculateLevel()
    {
        for (int level = Level; level < Managers.Data.StageExperienceInfoDictionary.Count; level++)
        {
            if (_experiecne < Managers.Data.StageExperienceInfoDictionary[level].NextLevelExperience)
            {
                MaxExp = Managers.Data.StageExperienceInfoDictionary[level].NextLevelExperience;
                return level;
            }

            _experiecne -= Managers.Data.StageExperienceInfoDictionary[level].NextLevelExperience;
        }

        int maxLevel = Managers.Data.StageExperienceInfoDictionary.Count;
        MaxExp = Managers.Data.StageExperienceInfoDictionary[maxLevel].NextLevelExperience;
        _experiecne = _experiecne >= MaxExp ? MaxExp : _experiecne;
        return maxLevel;
    }

    private IEnumerator CoPlayGameScene()
    {
        while (true)
        {
            if (State == Define.GameSceneState.Play)
            {
                GameTimer = Mathf.Clamp(GameTimer += Time.deltaTime, 0.0f, Mathf.Infinity);
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
        yield return new WaitForSeconds(2.0f); // Popup delay
        Managers.UI.ShowPopupUI<UI_GameOverPopup>();
    }

    public void ReplayGame()
    {
        Managers.Object.Clear();
        InitializeGameSettings();
    }

    public override void Clear()
    {
        base.Clear();
        StopAllCoroutines();
    }
}
