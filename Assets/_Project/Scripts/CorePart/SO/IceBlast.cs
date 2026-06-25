using UnityEngine;

[CreateAssetMenu(menuName = "CoreParts/IceBlast")]
public class IceBlast : CorePart
{
    [SerializeField] private float slowMultipler = 0.5f;
    [SerializeField] private GameObject iceTrail;
     

    public override void ApplyProjectileEffect(Projectile proj)
    {
        proj.AddEffect(new IceBlastProjectileEffect(slowMultipler));
    }
}
