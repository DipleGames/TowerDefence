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
        TowerModel tower = GetComponentInParent<TowerModel>();
        MonsterController targetMC = _currentTarget.GetComponent<MonsterController>();

        proj.transform.position = transform.position;
        _damage = tower.attackPower;

        _damage = DamageCalculator(tower, targetMC, _damage);

        proj.InitProj(_currentTarget, _damage); // 투사체 초기화

        foreach (CorePart part in tower.equippedPartList) // 투사체에 효과 적용
        {
            part.ApplyProjectileEffect(proj);
        }
    }

    public float DamageCalculator(TowerModel tower, MonsterController targetMC, float damage)
    {
        int ran = Random.Range(0,100);
        if(ran < tower.criticalProb * 100) // 크리티컬이 터지면 데미지 두배
        {
            damage *= 2;
        }

        if(targetMC.isGlacial && TowerManager.Instance.IsGlacialAugment)
        {
            damage *= 1.5f;
        }

        return damage;
    }
  
}
