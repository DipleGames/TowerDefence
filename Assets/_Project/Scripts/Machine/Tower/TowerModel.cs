using System.Collections.Generic;
using UnityEngine;

public class TowerModel : MonoBehaviour
{
    [Header("타워 기본 정보")]
    public TowerInfo towerInfo;

    [Header("타워 현재 정보")]
    public float attackPower; // 최종 공격 데미지
    public float attackSpeed; // 최종 공격 속도
    public float criticalProb; // 최종 크리티컬 확률
    public float maxFuelCapacity; // 최종 연료 최대양

    [Header("타워에 장착된 파츠")]
    public List<CorePart> equippedPartList = new();

    public void EquipCorePart(CorePart corePart, bool isMerge)
    {
        if (corePart == null) return;

        if (equippedPartList.Contains(corePart))
            return;

        equippedPartList.Add(corePart);

        corePart.OnEquip(this);
        StartCoroutine(corePart.EquipCoroutine(this));

         Debug.Log("타워에 코어파츠 장착!");

        if(!isMerge)
        {
            AudioManager.Instance.PlaySFX(corePart.equipSFX);
        }
        CorePartsManager.Instance.ownedCorePartList.Remove(corePart);
    }
}
