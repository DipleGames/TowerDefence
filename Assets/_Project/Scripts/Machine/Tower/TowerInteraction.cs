using UnityEngine;

public class TowerInteraction : MonoBehaviour
{
    public GameObject uiRoot;
    private TowerStateMachine _towerStateMachine;
    private TowerModel _towerModel;

    void Awake()
    {
        _towerStateMachine = GetComponent<TowerStateMachine>();
        _towerModel = GetComponent<TowerModel>();
    }

    void Update()
    {
        if (_towerStateMachine == null) return;
        if (_towerStateMachine.towerState != MachineState.InActive) return;

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.transform == transform || hit.collider.transform.IsChildOf(transform))
                {
                    uiRoot.SetActive(!uiRoot.activeSelf);
                }
            }
        }
    }

    void OnMouseEnter()
    {
        if (ViewManager.Instance.augmentView.isAugmentUIActive) return;

        HUDManager.Instance.SwitchHUD(ViewManager.Instance.towerView.towerStatePanel, true);
        ViewManager.Instance.towerView.UpdateTowerStatText(_towerStateMachine);
        ViewManager.Instance.towerView.UpdatePossibilityOfPowerDownText(_towerStateMachine);
        ViewManager.Instance.towerView.UpdateCorePartImage(_towerModel);
        ViewManager.Instance.towerView.StartFuelView(_towerStateMachine);
        ViewManager.Instance.towerView.StartPowerView(_towerStateMachine);
    }

    void OnMouseExit()
    {
        if (ViewManager.Instance.augmentView.isAugmentUIActive) return;

        HUDManager.Instance.SwitchHUD(ViewManager.Instance.towerView.towerStatePanel, false);
        ViewManager.Instance.towerView.StopFuelView();
        ViewManager.Instance.towerView.StopPowerView();
    }
}