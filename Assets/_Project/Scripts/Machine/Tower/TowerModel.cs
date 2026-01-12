using UnityEngine;
using System.Collections.Generic;

public class TowerModel : MonoBehaviour
{
    [Header("타워 정보")]
    public float detectRange; // 감지 영역
    public float attackDamage; // 공격 데미지
    public float attackDelay; // 공격 딜레이
    public int repairRequiredCost; // 고치는데 필요한 비용

    
    [Header("타워가 감지한 몬스터 목록")]
    public List<MonsterController> monsters = new();
}
