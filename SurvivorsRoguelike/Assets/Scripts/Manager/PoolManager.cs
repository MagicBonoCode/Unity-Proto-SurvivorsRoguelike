using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager
{
    #region Pool
    private class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; private set; }

        private Stack<Poolable> _poolStack = new Stack<Poolable>();

        public void Init(GameObject original, int count = 1)
        {
            Original = original;

            Root = new GameObject().transform;
            Root.name = $"{original.name}._Root";

            for (int i = 0; i < count; i++)
            {
                Push(Create());
            }
        }

        private Poolable Create()
        {
            GameObject gameObject = Object.Instantiate(Original);
            gameObject.name = Original.name;

            return gameObject.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null)
            {
                return;
            }

            poolable.transform.SetParent(Root);
            poolable.gameObject.SetActive(false);
            poolable.IsUsing = false;

            _poolStack.Push(poolable);
        }

        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (_poolStack.Count > 0)
            {
                poolable = _poolStack.Pop();
            }
            else
            {
                poolable = Create();
            }

            poolable.gameObject.SetActive(true);

            if (parent == null)
            {
                poolable.transform.SetParent(Managers.Scene.GetCurrentScene<BaseScene>().transform);
            }

            poolable.transform.SetParent(parent);
            poolable.IsUsing = true;
            return poolable;
        }
    }
    #endregion

    private Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    private Transform _root = null;

    public void Init()
    {
        if (_root == null)
        {
            _root = new GameObject { name = "_Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    private void CreatePool(GameObject original, int count = 1)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _root;

        _pool.Add(original.name, pool);
    }

    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;
        if (_pool.ContainsKey(name) == false)
        {
            Object.Destroy(poolable.gameObject);
            return;
        }

        _pool[name].Push(poolable);
    }

    public Poolable Pop(GameObject original, Transform parent = null)
    {
        if (_pool.ContainsKey(original.name) == false)
        {
            CreatePool(original);
        }

        return _pool[original.name].Pop(parent);
    }

    public GameObject GetOriginal(string name)
    {
        if (_pool.ContainsKey(name) == false)
        {
            return null;
        }

        return _pool[name].Original;
    }

    public void Clear()
    {
        foreach (Transform child in _root)
        {
            Object.Destroy(child.gameObject);
        }

        _pool.Clear();
    }
}
