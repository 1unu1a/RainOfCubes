using UnityEngine;
using System.Collections.Generic;

public class ObjectPool<T> where T : Component, IPoolObject
{
    private readonly Queue<T> _pool = new();
    private T _prefab;
    private Transform _root;

    public void Init(T prefab, int count)
    {
        _prefab = prefab;
        _root = new GameObject($"{typeof(T).Name}_Pool").transform;

        for (int i = 0; i < count; i++)
        {
            T instance = Object.Instantiate(_prefab, _root);
            instance.gameObject.SetActive(false);
            _pool.Enqueue(instance);
        }
    }

    public T Get()
    {
        if (_pool.Count == 0)
        {
            AddObjectToPool();
        }

        T obj = _pool.Dequeue();
        obj.gameObject.SetActive(true);
        obj.Init();
        return obj;
    }

    public void Return(T obj)
    {
        obj.DeInit();
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(_root);
        _pool.Enqueue(obj);
    }

    private void AddObjectToPool()
    {
        T instance = Object.Instantiate(_prefab, _root);
        instance.gameObject.SetActive(false);
        _pool.Enqueue(instance);
    }
}