using UnityEngine;

[CreateAssetMenu(menuName = "CoreParts/ThunderBolt")]
public class ThunderBolt : CoreParts
{
    [SerializeField] private float thunderDamage = 5f;
    [SerializeField] private float stunTime = 0.5f;
    [SerializeField] private GameObject spark;
     

    public override void ApplyProjectileEffect(Projectile proj)
    {
        proj.AddEffect(new ThunderProjectileEffect(thunderDamage, stunTime));
    }
}