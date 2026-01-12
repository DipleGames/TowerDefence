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

    /// <summary>
    /// 연료 공급
    /// </summary>
    public void TryFuelSupply(TowerStateMachine towerStateMachine, TowerModel towerModel)
    {
        StartCoroutine(FuelSupply(towerStateMachine, towerModel));
    }

    public IEnumerator FuelSupply(TowerStateMachine towerStateMachine, TowerModel towerModel)
    {
        if(GoldManager.Instance.CurrGold < towerModel.fuelSupplyRequiredCost) yield break;

        Debug.Log($"{towerStateMachine}에 연료를 공급하고있습니다..");
        GoldManager.Instance.SubtractGold(towerModel.fuelSupplyRequiredCost);
        yield return new WaitForSeconds(2f);
        towerStateMachine.currFuelCapacity = towerStateMachine.maxFuelCapacity;
        towerStateMachine.isFuelShortage = false;
        yield break;
    }

    /// <summary>
    /// 파워 수리
    /// </summary>
    public void TryRepairPower(TowerStateMachine towerStateMachine, TowerModel towerModel)
    {
        StartCoroutine(RepairPower(towerStateMachine, towerModel));
    }

    public IEnumerator RepairPower(TowerStateMachine towerStateMachine, TowerModel towerModel)
    {
        if(GoldManager.Instance.CurrGold < towerModel.repairPowerRequiredCost) yield break;

        Debug.Log($"{towerStateMachine}에 파워를 수리하고있습니다..");
        GoldManager.Instance.SubtractGold(towerModel.repairPowerRequiredCost);
        yield return new WaitForSeconds(2f);
        towerStateMachine.isPowerDown = false;
        yield break;
    }
}
