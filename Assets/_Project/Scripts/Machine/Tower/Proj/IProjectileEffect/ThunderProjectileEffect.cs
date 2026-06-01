using UnityEngine;

public class ThunderProjectileEffect : IProjectileEffect
{
    private float _thunderDamage;
    private float _stunTime;

    public ThunderProjectileEffect(float thunderDamage, float stunTime)
    {
        _thunderDamage = thunderDamage;
        _stunTime = stunTime;
    }

    public void Apply(MonsterController target, float baseDamage)
    {
        target.TakeDamage(_thunderDamage);
        Debug.Log($"효과가 적용되서{_thunderDamage}의 추가데미지");
    }
}