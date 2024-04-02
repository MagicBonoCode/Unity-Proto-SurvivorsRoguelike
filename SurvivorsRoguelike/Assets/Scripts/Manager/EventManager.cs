using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    private Dictionary<string, Action> _event;

    public void Init()
    {
        if (_event == null)
        {
            _event = new Dictionary<string, Action>();
        }
    }

    public void AddEvent(string eventName, Action listener)
    {
        Action thisEvent;
        if (_event.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            _event[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            _event.Add(eventName, thisEvent);
        }
    }

    public void RemoveEvent(string eventName, Action listener)
    {
        if (_event == null)
        {
            return;
        }

        Action thisEvent;
        if (_event.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            _event[eventName] = thisEvent;
        }
    }

    public void TriggerEvent(string eventName)
    {
        Action thisEvent;
        if (_event.TryGetValue(eventName, out thisEvent))
        {
            thisEvent?.Invoke();
        }
    }

    public void Clear()
    {
        _event.Clear();
    }
}
