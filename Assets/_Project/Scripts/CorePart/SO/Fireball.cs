using UnityEngine;

[CreateAssetMenu(menuName = "CoreParts/Fireball")]
public class Fireball : CorePart
{
    [SerializeField] private float dotDamage = 5f;
    [SerializeField] private GameObject fireTrail;
     

    public override void ApplyProjectileEffect(Projectile proj)
    {
        proj.AddEffect(new FireballProjectileEffect(dotDamage));
    }
}