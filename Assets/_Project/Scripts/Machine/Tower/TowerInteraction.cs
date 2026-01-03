using Mono.Cecil.Cil;
using UnityEngine;

public class TowerInteraction : MonoBehaviour
{
    public GameObject uiRoot;
    private TowerStateMachine _towerStateMachine;

    void Awake()
    {
        _towerStateMachine = GetComponent<TowerStateMachine>();
    }

    void OnMouseDown()
    {
        if(_towerStateMachine.towerState == MachineState.InActive)
        {
            uiRoot.SetActive(!uiRoot.activeSelf);
        }
    }

}
