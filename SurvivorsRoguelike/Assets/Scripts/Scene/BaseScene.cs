using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
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
