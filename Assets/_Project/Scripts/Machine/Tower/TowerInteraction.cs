using UnityEngine;

public class TowerInteraction : MonoBehaviour
{
    public GameObject uiRoot; // 얘는 타워에 붙어있는 객체이기때문에 여기서 관리
    private TowerStateMachine _towerStateMachine;

    void Awake()
    {
        _towerStateMachine = GetComponent<TowerStateMachine>();
    }

    void OnMouseEnter()
    {
        if(ViewManager.Instance.augmentView.isAugmentUIActive) return;

        HUDManager.Instance.SwitchHUD(ViewManager.Instance.towerView.towerStatePanel, true);
        //ViewManager.Instance.towerView.UpdateTowerNameText(gameObject.name);
        ViewManager.Instance.towerView.UpdateTowerStatText(_towerStateMachine);
        ViewManager.Instance.towerView.UpdatePossibilityOfPowerDownTextt(_towerStateMachine);
        ViewManager.Instance.towerView.StartFuelView(_towerStateMachine);
        ViewManager.Instance.towerView.StartPowerView(_towerStateMachine);
    }

    void OnMouseExit()
    {
        if(ViewManager.Instance.augmentView.isAugmentUIActive) return;

        HUDManager.Instance.SwitchHUD(ViewManager.Instance.towerView.towerStatePanel, false);
        ViewManager.Instance.towerView.StopFuelView();
        ViewManager.Instance.towerView.StopPowerView();
    }


    void OnMouseDown()
    {
        if(_towerStateMachine.towerState == MachineState.InActive)
        {
            uiRoot.SetActive(!uiRoot.activeSelf);
        }
    }

}
