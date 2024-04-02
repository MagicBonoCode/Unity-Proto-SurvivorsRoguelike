using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObject : MonoBehaviour
{
    public Define.ObjectType ObjectType { get; protected set; }

    private bool _init = false;

    private void Awake()
    {
        Init();
    }

    protected virtual bool Init()
    {
        if (_init)
        {
            return false;
        }

        _init = true;
        return true;
    }

    private void Start()
    {
        StartObject();
    }

    protected virtual void StartObject() { }

    private void OnEnable()
    {
        OnEnableObject();
    }

    protected virtual void OnEnableObject() { }

    private void Update()
    {
        UpdateController();
    }

    protected virtual void UpdateController() { }

    private void FixedUpdate()
    {
        FixedUpdateController();
    }

    protected virtual void FixedUpdateController() { }

    private void LateUpdate()
    {
        LateUpdateController();
    }

    protected virtual void LateUpdateController() { }

    private void OnDisable()
    {
        OnDisableObject();
    }

    protected virtual void OnDisableObject() { }

    private void OnDestroy()
    {
        OnDestroyObject();
    }

    protected virtual void OnDestroyObject() { }
}
