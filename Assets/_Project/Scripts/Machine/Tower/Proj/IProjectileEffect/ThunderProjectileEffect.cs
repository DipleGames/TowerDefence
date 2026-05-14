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

    public void Apply(MonsterModel target, float baseDamage)
    {
        
    }
}