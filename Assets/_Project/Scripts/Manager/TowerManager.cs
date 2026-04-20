using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class TowerManager : SingleTon<TowerManager>
{
    public List<TowerStateMachine> towerList = new();
    public List<GameObject> towerRevolutionList = new();

 
    void Start()
    {
        UpdateTowerList();

        ViewManager.Instance.towerView.UpdateFuelSupplyCostText();
        ViewManager.Instance.towerView.UpdateRepairPowerCostText();
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

    public void TryAllFuelSupply()
    {
        foreach(TowerStateMachine tower in towerList)
        {
            if(tower.isFuelShortage)
            {
                StartCoroutine(FuelSupply(tower, tower.towerController.towerModel));
            }
        }
    }

    public IEnumerator FuelSupply(TowerStateMachine towerStateMachine, TowerModel towerModel)
    {
        if(GoldManager.Instance.CurrGold < towerModel.fuelSupplyRequiredCost) yield break;

        Debug.Log($"{towerStateMachine}에 연료를 공급하고있습니다..");
        GoldManager.Instance.SubtractGold(towerModel.fuelSupplyRequiredCost);
        yield return new WaitForSeconds(2f);
        towerStateMachine.currFuelCapacity = towerModel.maxFuelCapacity;
        towerStateMachine.isFuelShortage = false;
        ViewManager.Instance.towerView.UpdateFuelSupplyCostText();
        yield break;
    }

    /// <summary>
    /// 파워 수리
    /// </summary>
    public void TryRepairPower(TowerStateMachine towerStateMachine, TowerModel towerModel)
    {
        StartCoroutine(RepairPower(towerStateMachine, towerModel));
    }

    public void TryAllRepairPower()
    {
        foreach(TowerStateMachine tower in towerList)
        {
            if(tower.isPowerDown)
            {
                StartCoroutine(RepairPower(tower, tower.towerController.towerModel));
            }
        }
    }

    public IEnumerator RepairPower(TowerStateMachine towerStateMachine, TowerModel towerModel)
    {
        if(GoldManager.Instance.CurrGold < towerModel.repairPowerRequiredCost) yield break;

        Debug.Log($"{towerStateMachine}에 파워를 수리하고있습니다..");
        GoldManager.Instance.SubtractGold(towerModel.repairPowerRequiredCost);
        yield return new WaitForSeconds(2f);
        towerStateMachine.isPowerDown = false;
        ViewManager.Instance.towerView.UpdateRepairPowerCostText();
        yield break;
    }

    #region Augment
    public float AttackPowerRateBonus { get; private set; }
    public float AttackSpeedMultiplier { get; private set; } = 1f;
    public float FuelIncreaseRateBonus { get; private set; }
    public float ReducedPowerOutageChanceRateBonus { get; private set; }

    public void AddAttackPowerBonus(float value)
    {
        AttackPowerRateBonus += value;
    }

    public void AddAttackSpeedBonus(float value)
    {
        AttackSpeedMultiplier *= value;
    }

    public void AddFuelIncreaseBonus(float value)
    {
        FuelIncreaseRateBonus += value;
    }

    public void AddReducedPowerOutageChanceRateBonus(float value)
    {
        ReducedPowerOutageChanceRateBonus += value;
    }
    
    #endregion
}
