using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingleTon<PoolManager>
{
    [Header("Projectile")]
    [SerializeField] private Projectile _projPrefab;
    [SerializeField] private Transform _projParent;
    [SerializeField] private int _projPoolSize = 32;

    [Header("Hit Effect")]
    [SerializeField] private HitEffect _hitEffectPrefab;
    [SerializeField] private Transform _hitEffectParent;
    [SerializeField] private int _hitEffectPoolSize = 32;

    [Header("Fire Trail")]
    [SerializeField] private FireTrail _fireTrailPrefab;
    [SerializeField] private Transform _fireTrailParent;
    [SerializeField] private int _fireTrailPoolSize = 32;

    [Header("Ice Trail")]
    [SerializeField] private IceTrail _iceTrailPrefab;
    [SerializeField] private Transform _iceTrailParent;
    [SerializeField] private int _iceTrailPoolSize = 32;

    [Header("Spark")]
    [SerializeField] private Spark _sparkPrefab;
    [SerializeField] private Transform _sparkParent;
    [SerializeField] private int _sparkPoolSize = 32;

    private readonly Dictionary<Type, object> _pools = new();

    protected override void Awake()
    {
        base.Awake();

        Register(_projPrefab, _projParent, _projPoolSize);
        Register(_hitEffectPrefab, _hitEffectParent, _hitEffectPoolSize);
        Register(_fireTrailPrefab, _fireTrailParent, _fireTrailPoolSize);
        Register(_iceTrailPrefab, _iceTrailParent, _iceTrailPoolSize);
        Register(_sparkPrefab, _sparkParent, _sparkPoolSize);
    }

    private void Register<T>(T prefab, Transform parent, int poolSize) where T : MonoBehaviour
    {
        Type type = typeof(T);

        if (_pools.ContainsKey(type))
        {
            Debug.LogWarning($"{type.Name} 풀은 이미 등록되어 있습니다.");
            return;
        }

        ObjectPool<T> pool = new ObjectPool<T>(prefab, parent, poolSize);

        _pools.Add(type, pool);
    }

    public T Get<T>() where T : MonoBehaviour
    {
        Type type = typeof(T);

        if (!_pools.TryGetValue(type, out object poolObject))
        {
            Debug.LogError($"{type.Name} 풀이 등록되어 있지 않습니다.");
            return null;
        }

        if (poolObject is not ObjectPool<T> pool)
        {
            Debug.LogError($"{type.Name} 풀의 타입이 일치하지 않습니다.");
            return null;
        }

        return pool.Get();
    }

    public void Return<T>(T poolObject) where T : MonoBehaviour
    {
        if (poolObject == null)
        {
            Debug.LogWarning("반환하려는 오브젝트가 null입니다.");
            return;
        }

        Type type = typeof(T);

        if (!_pools.TryGetValue(type, out object registeredPool))
        {
            Debug.LogError($"{type.Name} 풀이 등록되어 있지 않습니다.");
            return;
        }

        if (registeredPool is not ObjectPool<T> pool)
        {
            Debug.LogError($"{type.Name} 풀의 타입이 일치하지 않습니다.");
            return;
        }

        pool.Return(poolObject);
    }
}