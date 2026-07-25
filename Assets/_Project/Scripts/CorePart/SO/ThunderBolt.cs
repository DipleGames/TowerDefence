using UnityEngine;

[CreateAssetMenu(menuName = "CoreParts/ThunderBolt")]
public class ThunderBolt : CorePart
{
    [SerializeField] private float thunderDamage = 5f;
    [SerializeField] private float stunTime = 0.5f;
    [SerializeField] private GameObject spark;
     
    public override string coreDescription => $"적을 타격할 시 적은 {stunTime}초간 기절상태에 빠지고 {thunderDamage}의 추가데미지를 받습니다. ";

    public override void ApplyProjectileEffect(Projectile proj)
    {
        proj.AddEffect(new ThunderProjectileEffect(thunderDamage, stunTime));
    }
}