using UnityEngine;

[CreateAssetMenu(menuName = "CoreParts/IceBlast")]
public class IceBlast : CorePart
{
    [SerializeField] private float slowMultipler = 0.5f;
    [SerializeField] private GameObject iceTrail;
     
    public override string coreDescription => $"적을 타격할 시 초당 적의 이동 속도를 {slowMultipler * 100}% 저하시키는 얼음 장판을 3초간 설치합니다. ";

    public override void ApplyProjectileEffect(Projectile proj)
    {
        proj.AddEffect(new IceBlastProjectileEffect(slowMultipler));
    }
}
