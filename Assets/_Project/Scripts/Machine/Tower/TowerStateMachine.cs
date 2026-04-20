using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
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
        while (true)
        {
            // 연료 소비
            towerController.ConsumeFuel();

            // 전원 체크 (5초마다 한 번) ---
            towerController.CheckPower();

            // --- 상태 전환 판정 ---
            StateTransitionDecision();

            yield return null; // 프레임 지연 (Update처럼 동작)
        }
    }

    void StateTransitionDecision()
    {
        if (isFuelShortage || isPowerDown) // 만약 연료가 부족하거나 파워가 다운되면
        {
            if (towerState != MachineState.InActive)
            {
                ChangeTowerState(MachineState.InActive);
            } 
        }
        else // 연료도 부족하지않고 파워도 다운되지않았다면
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
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        foreach(var mr in meshRenderers)
        {
            mr.material = Mat_Active;
        }
        StartCoroutine(towerController.AttackRoutine());
    }

    public override void ApplyInActiveState()
    {
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        foreach(var mr in meshRenderers)
        {
            mr.material = Mat_InActive;
        }
        Debug.Log("타워가 비활성화 됐습니다.");
    }
}
