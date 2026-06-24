using UnityEngine;

public class Spark : MonoBehaviour, IPoolable
{
    private float _thunderDamage;
    private ParticleSystem _ps;
    private bool _isReturning;

    private void Awake()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!_isReturning && !_ps.IsAlive(true))
        {
            _isReturning = true;
            PoolManager.Instance.ReturnSpark(this);
        }
    }

    public void InitSpark(float thunderDamage)
    {
        _thunderDamage = thunderDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        MonsterController monster = other.GetComponentInParent<MonsterController>();

        if (monster == null)
            return;

        IDamageable damageable = monster.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(_thunderDamage);
            Debug.Log($"{_thunderDamage} 주기");
        }
    }

    public void OnSpawnFromPool()
    {
        _isReturning = false;

        gameObject.SetActive(true);

        _ps.Clear();
        _ps.Play();
    }

    public void OnReturnToPool()
    {
        _isReturning = true;
        _thunderDamage = 0f;

        _ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        gameObject.SetActive(false);
    }
}