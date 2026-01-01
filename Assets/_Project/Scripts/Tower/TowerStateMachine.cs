using Unity.VisualScripting;
using UnityEngine;

public class TowerStateMachine : MonoBehaviour
{
    public enum TowerState { Active, InActive }

    public TowerState towerState = TowerState.Active;
    public TowerController towerController;

    [Header("타워 활성화 시간")]
    public float maxActiveTime;
    public float activeTime;

    [Header("타워 활성화 / 비활성화 머터리얼")]
    public Material Mat_Active;
    public Material Mat_InActive;

    void Start()
    {
        ApplyTowerState();
    }

    void ApplyTowerState()
    {
        switch(towerState)
        {
            case TowerState.Active:
                ApplyActiveState();
                break;
            case TowerState.InActive:
                ApplyInActiveState();
                break;
        }
    }

    public void ChangeTowerState(TowerState towerState)
    {
        this.towerState = towerState;
        ApplyTowerState();
    }

    void ApplyActiveState()
    {
        GetComponentInChildren<MeshRenderer>().material = Mat_Active;
        StartCoroutine(towerController.AttackRoutine());
    }

    void ApplyInActiveState()
    {
        GetComponentInChildren<MeshRenderer>().material = Mat_InActive;
        Debug.Log("타워가 비활성화 됐습니다.");
    }
}
