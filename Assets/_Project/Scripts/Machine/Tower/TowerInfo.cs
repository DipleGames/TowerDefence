using UnityEngine;

[CreateAssetMenu(fileName = "TowerInfo")]
public class TowerInfo : ScriptableObject
{
    [Header("타워 기본 정보")]
    public int towerLv;
    public float detectRange; // 감지 영역
    public float baseAttackPower; // 공격 데미지
    public float baseAttackSpeed; // 공격 속도
    public float baseCriticalProb;
    public float baseMaxFuelCapacity; // 기본 연료 최대양
    public float baseReducedPowerOutageChance; // 기본 고장 확률
    public int fuelSupplyRequiredCost; // 연료 주입 비용
    public int repairPowerRequiredCost; // 파워 수리하는데 필요한 비용
}
