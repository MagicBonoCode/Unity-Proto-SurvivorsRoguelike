using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return UnityEngine.Object.FindObjectOfType<BaseScene>(); } }

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
}
