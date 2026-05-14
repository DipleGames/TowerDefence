using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform _target;
    private float _projSpeed;
    private float _damage;
    private bool _isInit = false;

    [Header("적용된 효과")]
    [SerializeField] private List<IProjectileEffect> _effects = new();

    public void InitProj(Transform target, float damage)
    {
        _target = target;
        _projSpeed = 15f;
        _damage = damage;
        _isInit = true;
        _effects.Clear();
    }

    void Update()
    {
        if(!_isInit) return;

        if(_target == null)
        {
            Destroy(gameObject);
            return;
        }
        transform.position += (_target.position - transform.position).normalized * _projSpeed * Time.deltaTime;
    }

    public void AddEffect(IProjectileEffect effect)
    {
        _effects.Add(effect);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            other.GetComponentInParent<IDamageable>().TakeDamage(_damage);
            PoolManager.Instance.ReturnProj(this);
        }
    }
}
