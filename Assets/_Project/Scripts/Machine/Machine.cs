using UnityEngine;

public enum MachineState { Active , InActive }

public abstract class Machine : MonoBehaviour
{

    [Header("기계 활성화 시간")]
    public float maxActiveTime;
    public float activeTime;

    [Header("기계 활성화 / 비활성화 머터리얼")]
    public Material Mat_Active;
    public Material Mat_InActive;
    
    public abstract void ApplyActiveState();
    public abstract void ApplyInActiveState();
}
