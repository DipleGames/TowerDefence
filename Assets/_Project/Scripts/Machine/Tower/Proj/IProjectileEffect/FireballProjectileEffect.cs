using UnityEngine;

public class FireballProjectileEffect : IProjectileEffect
{
    private float _dotDamage;

    public FireballProjectileEffect(float dotDamage)
    {
        _dotDamage = dotDamage;
    }

    public void Apply(MonsterController target, float baseDamage)
    {
        
    }
}
