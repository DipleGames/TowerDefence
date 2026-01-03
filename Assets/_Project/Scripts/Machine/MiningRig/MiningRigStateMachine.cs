using System.Text;
using UnityEngine;

public class MiningRigStateMachine : Machine
{
    [Header("채굴기 상태")]
    public MachineState miningRigState = MachineState.Active;
    [Header("채굴기 컨트롤러")]
    public MiningRigController miningRigController;


    void Start()
    {
        ApplyMiningRigState();
    }

    void ApplyMiningRigState()
    {
        switch(miningRigState)
        {
            case MachineState.Active:
                ApplyActiveState();
                break;
            case MachineState.InActive:
                ApplyInActiveState();
                break;
        }
    }

    public void ChangeMiningRigState(MachineState miningRigState)
    {
        this.miningRigState = miningRigState;
        ApplyMiningRigState();
    }

    public override void ApplyActiveState()
    {
        GetComponentInChildren<MeshRenderer>().material = Mat_Active;
        StartCoroutine(miningRigController.MiningRoutine());
    }

    public override void ApplyInActiveState()
    {
        GetComponentInChildren<MeshRenderer>().material = Mat_InActive;
        Debug.Log("채굴기가 비활성화 됐습니다.");
    }
}
