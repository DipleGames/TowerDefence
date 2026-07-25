using System.Collections;
using System.ComponentModel;
using UnityEngine;

public abstract class CorePart : ScriptableObject
{
    [Header("코어 정보")]
    public int coreID;
    public string coreName;
    public Sprite coreImg;
    public AudioClip equipSFX;

    [TextArea(3, 10)]
    public abstract string coreDescription { get; }

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
