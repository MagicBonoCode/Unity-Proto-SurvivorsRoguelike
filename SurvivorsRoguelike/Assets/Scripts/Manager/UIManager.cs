using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager
{
    private int _order = 10;
    private Stack<UI_BasePopup> _popupStack = new Stack<UI_BasePopup>();

    public UI_BaseScene SceneUI { get; private set; }

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("_UI_Root");
            if (root == null)
            {
                root = new GameObject { name = "_UI_Root" };
            }

            return root;
        }
    }

    public void SetCanvas(GameObject gameObject, bool sort = true)
    {
        Canvas canvas = gameObject.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_BaseScene
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject gameObject = Managers.Resource.Instantiate($"{name}");
        T sceneUI = gameObject.GetOrAddComponent<T>();
        SceneUI = sceneUI;

        gameObject.transform.SetParent(Root.transform);

        return sceneUI;
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_BasePopup
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject gameObject = Managers.Resource.Instantiate($"{name}");
        T popup = gameObject.GetOrAddComponent<T>();
        _popupStack.Push(popup);

        gameObject.transform.SetParent(Root.transform);

        return popup;
    }

    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject gameObject = Managers.Resource.Instantiate($"{name}");
        if (parent != null)
        {
            gameObject.transform.SetParent(parent);
        }

        return gameObject.GetOrAddComponent<T>();
    }

    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }

        GameObject gameObject = Managers.Resource.Instantiate($"{name}");
        if (parent != null)
        {
            gameObject.transform.SetParent(parent);
        }

        Canvas canvas = gameObject.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return gameObject.GetOrAddComponent<T>();
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
        {
            return;
        }

        UI_BasePopup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        _order--;
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
        {
            ClosePopupUI();
        }
    }

    public void Clear()
    {
        CloseAllPopupUI();
        SceneUI = null;
    }
}
