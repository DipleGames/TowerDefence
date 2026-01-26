using System.Collections;
using UnityEngine;

public class TowerController : MonoBehaviour, IAttackable
{
    public TowerModel towerModel;
    public TowerStateMachine towerStateMachine;
    public SphereCollider detectSensor;

    void Awake()
    {
        InitTower();
    }

    void InitTower()
    {
        detectSensor.radius = towerModel.detectRange;
        towerStateMachine.currFuelCapacity = towerStateMachine.maxFuelCapacity;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            MonsterController monsterController = other.GetComponentInParent<MonsterController>();
            towerModel.monsters.Add(monsterController);
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
                int ran = Random.Range(0, 100);
                if (ran < towerStateMachine.possibilityOfPowerDown)
                {
                    towerStateMachine.isPowerDown = true;
                }
            }
            powerCheckTick = 0f;
        }
    }

    public IEnumerator AttackRoutine()
    {
        while(towerStateMachine.towerState == MachineState.Active)
        {
            foreach(var monster in towerModel.monsters)
            {
                monster.TakeDamage(towerModel.attackDamage);
            }
            
            yield return new WaitForSeconds(towerModel.attackDelay);
        }
        yield break;
    }
}
