using UnityEngine;

[CreateAssetMenu(menuName = "CoreParts/Fireball")]
public class Fireball : CoreParts
{
    [SerializeField] private float dotDamage = 5f;
    [SerializeField] private GameObject fireAura;
     

    public override void ApplyProjectileEffect(Projectile proj)
    {
        proj.AddEffect(new FireballProjectileEffect(dotDamage));
    }
}