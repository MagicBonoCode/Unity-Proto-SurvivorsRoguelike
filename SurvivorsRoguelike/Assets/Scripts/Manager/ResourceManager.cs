using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager
{
    private Dictionary<string, UnityEngine.Object> _resources = new Dictionary<string, UnityEngine.Object>();

    #region Addressable
    private void LoadAsync<T>(string key, Action<T> callback = null) where T : UnityEngine.Object
    {
        if (_resources.TryGetValue(key, out UnityEngine.Object resource))
        {
            callback?.Invoke(resource as T);
            return;
        }

        string loadKey = key;
        if (key.Contains(".sprite"))
        { 
            loadKey = $"{key}[{key.Replace(".sprite", "")}]";
        }

        var asyncOperation = Addressables.LoadAssetAsync<T>(loadKey);
        asyncOperation.Completed += (resourceOperation) =>
        {
            _resources.Add(key, resourceOperation.Result);
            callback?.Invoke(resourceOperation.Result);
        };
    }

    public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : UnityEngine.Object
    {
        var operationHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
        operationHandle.Completed += (resourceOperation) =>
        {
            int loadCount = 0;
            int totalCount = resourceOperation.Result.Count;

            foreach (var result in resourceOperation.Result)
            {
                LoadAsync<T>(result.PrimaryKey, (obj) =>
                {
                    loadCount++;
                    callback?.Invoke(result.PrimaryKey, loadCount, totalCount);
                });
            }
        };
    }
    #endregion

    public T Load<T>(string key) where T : UnityEngine.Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            GameObject gameObject = Managers.Pool.GetOriginal(key);
            if (gameObject != null)
            {
                return gameObject as T;
            }
        }

        if (_resources.TryGetValue(key, out UnityEngine.Object resource))
        { 
            return resource as T;
        }

        Debug.LogError($"{key}를 로드하지 못했습니다.");
        return null;
    }

    public GameObject Instantiate(string key, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"{key}");
        if (prefab == null)
        {
            Debug.LogError($"Prefab {key}을 로드하지 못했습니다.");
            return null;
        }

        if (prefab.GetComponent<Poolable>() != null)
        { 
            return Managers.Pool.Pop(prefab, parent).gameObject;
        }

        GameObject gameObject = UnityEngine.Object.Instantiate(prefab, parent);
        gameObject.name = prefab.name;
        return gameObject;
    }

    public void Destroy(GameObject gameObject)
    {
        if (gameObject == null)
        { 
            Debug.LogError($"삭제할 {gameObject.name} 가 없습니다.");
            return;
        }

        Poolable poolable = gameObject.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        UnityEngine.Object.Destroy(gameObject);
    }
}
