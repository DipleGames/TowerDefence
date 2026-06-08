using UnityEngine;

[CreateAssetMenu(menuName = "CoreParts/Fireball")]
public class Fireball : CoreParts
{
    [SerializeField] private float dotDamage = 5f;
     

    public override void ApplyProjectileEffect(Projectile proj)
    {
        proj.AddEffect(new FireballProjectileEffect(dotDamage));
    }
}