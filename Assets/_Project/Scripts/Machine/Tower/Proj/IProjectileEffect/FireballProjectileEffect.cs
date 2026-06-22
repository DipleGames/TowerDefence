using UnityEngine;

public class FireballProjectileEffect : IProjectileEffect
{
    private float _dotDamage;
    private GameObject _fireTrail;

    public FireballProjectileEffect(float dotDamage, GameObject fireTrail)
    {
        _dotDamage = dotDamage;
        _fireTrail = fireTrail;
    }

    public void Apply(MonsterController target, float baseDamage)
    {
        Debug.Log("파이어볼 적용");
        Object.Instantiate(_fireTrail,target.transform.position,Quaternion.identity);
    }
}
