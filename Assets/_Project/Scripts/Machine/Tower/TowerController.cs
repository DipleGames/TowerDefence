using System.Collections;
using UnityEngine;

public class TowerController : MonoBehaviour, IAttackable
{
    [Header("실제 타겟")]
    public MonsterController targetMC; 

    [Header("타워 컴퍼넌트")]
    public TowerModel towerModel;
    public TowerStateMachine towerStateMachine;
    public TowerStatCalculator towerStatCalculator;

    [Header("타워 부착물")]
    public Cannon cannon;
    public SphereCollider detectSensor;

    void Awake()
    {
        InitTower();
    }

    void InitTower()
    {
        detectSensor.radius = towerModel.detectRange;
        
        towerStatCalculator.RecalculateStats(this); // 스탯 계산
        towerStateMachine.currFuelCapacity = towerModel.maxFuelCapacity; // 연료 주입
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            MonsterController monsterController = other.GetComponentInParent<MonsterController>();

            if (monsterController != null && !towerModel.monsters.Contains(monsterController))
            {
                towerModel.monsters.Add(monsterController);
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            MonsterController monsterController = other.GetComponentInParent<MonsterController>();
            towerModel.monsters.Remove(monsterController);
        }
    }

    /// <summary>
    /// 연료 소비
    /// </summary>
    public void ConsumeFuel()
    {
        if (towerStateMachine.towerState == MachineState.Active)
        {
            towerStateMachine.currFuelCapacity -= Time.deltaTime;
            if (towerStateMachine.currFuelCapacity <= 0)
            {
                towerStateMachine.isFuelShortage = true;
                ViewManager.Instance.towerView.UpdateFuelSupplyCostText();
            }
        }
    }

    /// <summary>
    /// 파워 체크
    /// </summary>
    float powerCheckTick = 0f;
    public void CheckPower()
    {
        powerCheckTick += Time.deltaTime;
        if (powerCheckTick >= 5f)
        {
            if (towerStateMachine.towerState == MachineState.Active)
            {
                float ran = Random.Range(0f, 100f);
                if (ran < towerStateMachine.currPossibilityOfPowerDown)
                {
                    towerStateMachine.isPowerDown = true;
                    ViewManager.Instance.towerView.UpdateRepairPowerCostText();
                }
            }
            powerCheckTick = 0f;
        }
    }

    public IEnumerator AttackRoutine()
    {
        while (towerStateMachine.towerState == MachineState.Active)
        {
            MonsterController target = GetClosestMonster();
            targetMC = target;

            if (targetMC != null)
            {
                cannon.SetTarget(targetMC.transform);
                cannon.Shot();
            }
            else
            {
                cannon.ClearTarget();
            }

            yield return new WaitForSeconds(towerModel.attackSpeed);
        }
    }


    private MonsterController GetClosestMonster()
    {
        MonsterController closest = null;
        float minDistSqr = float.MaxValue;

        for (int i = towerModel.monsters.Count - 1; i >= 0; i--)
        {
            MonsterController m = towerModel.monsters[i];

            // 이미 죽었거나 파괴된 몬스터 정리
            if (m == null || !m.gameObject.activeInHierarchy)
            {
                towerModel.monsters.RemoveAt(i);
                continue;
            }

            float distSqr = (m.transform.position - transform.position).sqrMagnitude;

            if (distSqr < minDistSqr)
            {
                minDistSqr = distSqr;
                closest = m;
            }
        }

        return closest;
    }

}
