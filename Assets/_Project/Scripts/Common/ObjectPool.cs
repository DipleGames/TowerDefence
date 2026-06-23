using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component, IPoolable
{
    private readonly Queue<T> _pool = new Queue<T>();
    private readonly T _prefab;
    private readonly Transform _parent;

    public ObjectPool(T prefab, Transform parent, int size)
    {
        _prefab = prefab;
        _parent = parent;

        for (int i = 0; i < size; i++)
        {
            T instance = Object.Instantiate(_prefab, _parent);
            instance.OnReturnToPool();
            _pool.Enqueue(instance);
        }
    }

    public T Get()
    {
        T obj = _pool.Dequeue();
        obj.OnSpawnFromPool();
        return obj;
    }

    public void Return(T obj)
    {
        obj.OnReturnToPool();
        _pool.Enqueue(obj);
    }
}