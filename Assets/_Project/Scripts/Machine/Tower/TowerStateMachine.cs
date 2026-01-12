using UnityEngine;
using System.Collections;
public class TowerStateMachine : Machine
{
    [Header("타워 상태")]
    public MachineState towerState = MachineState.Active;
    [Header("타워 컨트롤러")]
    public TowerController towerController;

    void Start()
    {
        // 처음 시작 시 상태 설정
        ChangeTowerState(MachineState.Active);
        
        // 상태 감시 로직 시작
        StartCoroutine(MonitorTowerConditions());
    }

    // 1. 비활성화/활성화 판정 로직
    IEnumerator MonitorTowerConditions()
    {
        float powerCheckTick = 0f;

        while (true)
        {
            // 연료 체크 
            if (towerState == MachineState.Active)
            {
                currFuelCapacity -= Time.deltaTime;
                if (currFuelCapacity <= 0)
                {
                    isFuelShortage = true;
                }
            }

            // 전원 체크 (5초마다 한 번) ---
            powerCheckTick += Time.deltaTime;
            if (powerCheckTick >= 5f)
            {
                if (towerState == MachineState.Active)
                {
                    int ran = Random.Range(0, 100);
                    if (ran < possibilityOfPowerDown)
                    {
                        isPowerDown = true;
                    }
                }
                powerCheckTick = 0f;
            }

            // --- 상태 전환 판정 ---
            StateTransitionDecision();

            yield return null; // 프레임 지연 (Update처럼 동작)
        }
    }

    void StateTransitionDecision()
    {
        if (isFuelShortage || isPowerDown)
        {
            if (towerState != MachineState.InActive)
            {
                ChangeTowerState(MachineState.InActive);
            } 
        }
        else
        {
            if (towerState != MachineState.Active)
            {

                ChangeTowerState(MachineState.Active);
            } 
        }
    }

    public void ChangeTowerState(MachineState towerState)
    {
        this.towerState = towerState;
        
        switch(towerState)
        {
            case MachineState.Active:
                ApplyActiveState();
                break;
            case MachineState.InActive:
                ApplyInActiveState();
                break;
        }
    }

    public override void ApplyActiveState()
    {
        GetComponentInChildren<MeshRenderer>().material = Mat_Active;
        StartCoroutine(towerController.AttackRoutine());
    }

    public override void ApplyInActiveState()
    {
        GetComponentInChildren<MeshRenderer>().material = Mat_InActive;
        Debug.Log("타워가 비활성화 됐습니다.");
    }
}
