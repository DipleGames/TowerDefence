using UnityEngine;
using System.Collections.Generic;

public class TowerModel : MonoBehaviour
{
    [Header("타워 정보")]
    public float detectRange;
    public float attackDamage;
    
    [Header("타워가 감지한 몬스터 목록")]
    public List<MonsterController> monsters = new();
}
