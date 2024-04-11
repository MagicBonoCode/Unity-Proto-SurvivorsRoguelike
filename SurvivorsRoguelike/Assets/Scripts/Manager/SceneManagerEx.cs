using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    private BaseScene _currentScene;
    
    public T GetCurrentScene<T>() where T : BaseScene
    {
        return _currentScene as T;
    }

    public void SetCurrentScene(BaseScene scene)
    { 
        _currentScene = scene;
    }

    public void LoadScene(Define.Scene type, bool isAsync = true)
    {
        Managers.Clear();

        if (isAsync)
        {
            SceneManager.LoadSceneAsync(GetSceneName(type));
        }
        else
        { 
            SceneManager.LoadScene(GetSceneName(type));
        }
    }

    private string GetSceneName(Define.Scene type)
    {
        string name = Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear()
    {
        _currentScene.Clear();
    }
}
