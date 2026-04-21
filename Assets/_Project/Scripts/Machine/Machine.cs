using UnityEngine;

public enum MachineState { Active , InActive }

public abstract class Machine : MonoBehaviour
{
    [Header("현재 연료량")]
    public float currFuelCapacity;

    [Header("현재 파워 차단 확률")]
    public float currPossibilityOfPowerDown;

    [Header("고장 상태")]
    public bool isFuelShortage = false;
    public bool isPowerDown = false;

    [Header("기계 활성화 / 비활성화 머터리얼")]
    public Material Mat_Active;
    public Material Mat_InActive;

    
    public abstract void ApplyActiveState();
    public abstract void ApplyInActiveState();
}
