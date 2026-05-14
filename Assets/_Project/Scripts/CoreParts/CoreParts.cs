using System.Collections;
using UnityEngine;

public abstract class CoreParts : ScriptableObject
{
    [Header("카드 정보")]
    public int coreID;
    public string coreName;
    public string coreDesc;
    public Sprite coreImg;

    // 탈부착시 능력치 증가 파츠
    public virtual void OnEquip(TowerModel tower) { }

    public virtual void OnUnequip(TowerModel tower) { }

    // 오라나 버프같은거 재생시키는 파츠
    public virtual IEnumerator EquipCoroutine(TowerModel tower)
    {
        yield break;
    }

    // 공격에 특수 기능 들어가는 파츠
    public virtual void ApplyProjectileEffect(Projectile proj) { }
}
