using UnityEngine;

public enum MachineState { Active , InActive }

public abstract class Machine : MonoBehaviour
{

    [Header("기계 연료 량")]
    public float maxFuelCapacity;
    public float currFuelCapacity;

    [Header("파워 차단 관련")]
    public int possibilityOfPowerDown;

    [Header("고장 상태")]
    public bool isFuelShortage = false;
    public bool isPowerDown = false;

    [Header("기계 활성화 / 비활성화 머터리얼")]
    public Material Mat_Active;
    public Material Mat_InActive;

    
    public abstract void ApplyActiveState();
    public abstract void ApplyInActiveState();
}
