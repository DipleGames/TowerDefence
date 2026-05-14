using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform _cannon;
    [SerializeField] private Projectile _proj;
    [SerializeField] private float _damage = -1f;

    private Transform _currentTarget;


    public void SetTarget(Transform target)
    {
        _currentTarget = target;
    }

    public void ClearTarget()
    {
        _currentTarget = null;
    }

    public void Shot()
    {
        Projectile proj = PoolManager.Instance.GetProj();

        proj.transform.position = transform.position;

        TowerModel tower = GetComponentInParent<TowerModel>();
        float damage = tower.attackPower;

        proj.InitProj(_currentTarget, damage);
    }
  
}
