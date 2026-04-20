using UnityEngine;
using System.Collections.Generic;

public class TowerModel : MonoBehaviour
{
    [Header("타워 기본 정보")]
    public float detectRange; // 감지 영역
    public float baseAttackPower; // 공격 데미지
    public float baseAttackSpeed; // 공격 딜레이
    public float baseMaxFuelCapacity; // 기본 연료 최대양
    public float baseReducedPowerOutageChance; // 기본 고장 확률
    public int fuelSupplyRequiredCost; // 연료 주입 비용
    public int repairPowerRequiredCost; // 파워 수리하는데 필요한 비용

    [Header("타워 현재 정보")]
    public int towerLv = 0;
    public float attackPower; // 최종 공격 데미지
    public float attackSpeed; // 최종 공격 딜레이
    public float maxFuelCapacity; // 최종 연료 최대양

    
    [Header("타워가 감지한 몬스터 목록")]
    public List<MonsterController> monsters = new();
}
