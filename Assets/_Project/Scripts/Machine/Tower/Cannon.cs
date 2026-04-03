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
        Projectile proj = PoolManager.Instance.GetProj(); // 풀에서 총알 가져와서
 
        proj.gameObject.transform.position = transform.position; // 위치를 캐논으로 지정시키고
        _damage = GetComponentInParent<TowerModel>().attackPower;
        proj.InitProj(_currentTarget, _damage); // 정보를 초기화
    }
  
}
