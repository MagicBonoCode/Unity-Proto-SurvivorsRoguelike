using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IntroScene : BaseScene
{
    private int _totalCount;
    private int _count;

    private void OnGUI()
    {
        float percentage = ((float)_count / _totalCount) * 100.0f;
        GUI.skin.label.fontSize = 50;
        GUI.Label(new Rect(20, 20, 500, 500), $"Resource loading{percentage.ToString("0.00")}%.");
    }

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.IntroScene;

        Managers.Resource.LoadAllAsync<Object>("PreLoad", (key, count, totalCount) =>
        {
            _totalCount = totalCount;
            _count = count;

            if (count == totalCount)
            {
                Object eventSystem = GameObject.FindObjectOfType(typeof(EventSystem));
                if (eventSystem == null)
                {
                    eventSystem = Managers.Resource.Instantiate("EventSystem.prefab");
                    eventSystem.name = "_EventSystem";
                    DontDestroyOnLoad(eventSystem);
                }

                // Core
                Managers.Data.Init();
                Managers.Pool.Init();
                Managers.Event.Init();

                Managers.UI.ShowSceneUI<UI_IntroScene>();
            }
        });
    }
}
