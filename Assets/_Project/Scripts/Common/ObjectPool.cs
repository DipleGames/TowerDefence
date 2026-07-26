using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly T _prefab;
    private readonly Transform _parent;
    private readonly Queue<T> _pool = new();

    public ObjectPool(T prefab, Transform parent, int initialSize)
    {
        _prefab = prefab;
        _parent = parent;

        for (int i = 0; i < initialSize; i++)
        {
            T instance = CreateObject();
            _pool.Enqueue(instance);
        }
    }

    private T CreateObject()
    {
        T instance = Object.Instantiate(_prefab, _parent);
        instance.gameObject.SetActive(false);

        return instance;
    }

    public T Get()
    {
        T instance;

        if (_pool.Count > 0)
        {
            instance = _pool.Dequeue();
        }
        else
        {
            instance = CreateObject();
        }

        instance.gameObject.SetActive(true);

        return instance;
    }

    public void Return(T instance)
    {
        instance.gameObject.SetActive(false);
        instance.transform.SetParent(_parent);
        _pool.Enqueue(instance);
    }
}