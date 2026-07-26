using UnityEngine;

public class FireballProjectileEffect : IProjectileEffect
{
    private float _dotDamage;

    public FireballProjectileEffect(float dotDamage)
    {
        _dotDamage = dotDamage;
    }

    public void Apply(MonsterController target)
    {
        Debug.Log("파이어볼 적용");

        FireTrail fireTrail = PoolManager.Instance.Get<FireTrail>();
        fireTrail.InitFireTrail(_dotDamage); // 불 잔상 초기화

        fireTrail.transform.position = target.transform.position;
        fireTrail.transform.rotation = Quaternion.identity;
    }
}
