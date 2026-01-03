using System.Collections;
using UnityEngine;

public class TowerController : MonoBehaviour, IAttackable
{
    public TowerModel towerModel;
    public TowerStateMachine towerStateMachine;
    public SphereCollider detectSensor;

    void Awake()
    {
        detectSensor.radius = towerModel.detectRange;
        towerStateMachine.activeTime = 0f;
    }

    void Update()
    {
        if(towerStateMachine.towerState == MachineState.Active)
        {
            towerStateMachine.activeTime += Time.deltaTime;

            if(towerStateMachine.activeTime >= towerStateMachine.maxActiveTime)
            {
                towerStateMachine.ChangeTowerState(MachineState.InActive);  
                towerStateMachine.activeTime = 0f;
            } 
        }
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
