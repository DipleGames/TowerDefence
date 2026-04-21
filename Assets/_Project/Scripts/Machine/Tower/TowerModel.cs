using UnityEngine;
using System.Collections.Generic;

public class TowerModel : MonoBehaviour
{
    [Header("타워 기본 정보")]
    public TowerInfo towerInfo;

    [Header("타워 현재 정보")]
    public int towerLv = 0;
    public float attackPower; // 최종 공격 데미지
    public float attackSpeed; // 최종 공격 딜레이
    public float maxFuelCapacity; // 최종 연료 최대양

    
    [Header("타워가 감지한 몬스터 목록")]
    public List<MonsterController> monsters = new();
}
