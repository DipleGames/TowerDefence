using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform _target;
    private float _projSpeed;
    private float _damage;
    private bool _isInit = false;

    public void InitProj(Transform target, float damage)
    {
        _target = target;
        _projSpeed = 10f;
        _damage = damage;
        _isInit = true;
    }

    void Update()
    {
        if(!_isInit) return;

        if(_target == null) Destroy(gameObject);
        transform.position += (_target.position - transform.position).normalized * _projSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            other.GetComponentInParent<MonsterController>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
