using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TowerManager : SingleTon<TowerManager>
{
    public List<TowerStateMachine> towerList = new();

    void Start()
    {
        UpdateTowerList();
    }

    public void UpdateTowerList()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        towerList.Clear();

        foreach(GameObject tower in towers)
        {
            tower.GetComponentInChildren<Canvas>(true).worldCamera = Camera.main;
            TowerStateMachine towerStateMachine = tower.GetComponent<TowerStateMachine>();
            towerList.Add(towerStateMachine);
        }
    }

    public void TryRepair(TowerStateMachine towerStateMachine)
    {
        StartCoroutine(RepairTower(towerStateMachine));
    }

    public IEnumerator RepairTower(TowerStateMachine towerStateMachine)
    {
        if(GoldManager.Instance.CurrGold < 20) yield break;

        Debug.Log($"{towerStateMachine}를 수리하고있습니다..");
        GoldManager.Instance.SubtractGold(20);
        yield return new WaitForSeconds(2f);
        towerStateMachine.ChangeTowerState(MachineState.Active);
        yield break;
    }
}
