using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable
{
    private Transform _target;
    private float _projSpeed;
    private float _damage;
    private bool _isInit;

    private List<IProjectileEffect> _effects = new();

    public void InitProj(Transform target, float damage)
    {
        _target = target;
        _projSpeed = 15f;
        _damage = damage;
        _isInit = true;
        _effects.Clear();
    }

    private void Update()
    {
        if (!_isInit) return;

        if (_target == null)
        {
            PoolManager.Instance.ReturnProj(this);
            return;
        }

        Vector3 dir = (_target.position - transform.position).normalized;
        transform.position += dir * _projSpeed * Time.deltaTime;
    }

    public void AddEffect(IProjectileEffect effect)
    {
        _effects.Add(effect);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Monster"))
            return;

        IDamageable damageable = other.GetComponentInParent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(_damage);
        }

        MonsterController monster = other.GetComponentInParent<MonsterController>();

        if (monster != null)
        {
            foreach (IProjectileEffect effect in _effects)
            {
                effect.Apply(monster);
            }
        }

        PoolManager.Instance.ReturnProj(this);
    }

    public void OnSpawnFromPool()
    {
        gameObject.SetActive(true);
    }

    public void OnReturnToPool()
    {
        _target = null;
        _damage = 0f;
        _projSpeed = 0f;
        _isInit = false;
        _effects.Clear();

        gameObject.SetActive(false);
    }
}