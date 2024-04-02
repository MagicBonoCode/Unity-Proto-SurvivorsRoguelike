using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.None;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
    }

    public virtual void Clear()
    { 
    }
}
