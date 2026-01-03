using Unity.VisualScripting;
using UnityEngine;

public class TowerStateMachine : Machine
{
    [Header("타워 상태")]
    public MachineState towerState = MachineState.Active;
    [Header("타워 컨트롤러")]
    public TowerController towerController;

    void Start()
    {
        ApplyTowerState();
    }

    void ApplyTowerState()
    {
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

    public void ChangeTowerState(MachineState towerState)
    {
        this.towerState = towerState;
        ApplyTowerState();
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
