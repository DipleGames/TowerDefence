using UnityEngine;

[CreateAssetMenu(menuName = "CoreParts/Fireball")]
public class Fireball : CorePart
{
    [SerializeField] private float dotDamage = 5f;
    [SerializeField] private GameObject fireTrail;
     
    public override string coreDescription => $"적을 타격할 시 초당 {dotDamage} 데미지를 주는 화염 장판을 3초간 설치합니다. ";

    public override void ApplyProjectileEffect(Projectile proj)
    {
        proj.AddEffect(new FireballProjectileEffect(dotDamage));
    }
}