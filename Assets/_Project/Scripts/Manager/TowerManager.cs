using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class TowerManager : SingleTon<TowerManager>
{
    public List<TowerController> towerList = new();
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
            TowerController towerController = tower.GetComponent<TowerController>();
            towerList.Add(towerController);
        }
    }

    public void SpawnTower()
    {
        
    }


    /// <summary>
    /// 연료 공급
    /// </summary>
    public void TryFuelSupply(TowerController towerController, TowerModel towerModel)
    {
        StartCoroutine(FuelSupply(towerController, towerModel));
    }

    public void TryAllFuelSupply()
    {
        foreach(TowerController tower in towerList)
        {
            if(tower.towerStateMachine.isFuelShortage)
            {
                StartCoroutine(FuelSupply(tower, tower.towerModel));
            }
        }
    }

    public IEnumerator FuelSupply(TowerController towerController, TowerModel towerModel)
    {
        if(GoldManager.Instance.CurrGold < towerModel.towerInfo.fuelSupplyRequiredCost) yield break;

        Debug.Log($"{towerController.towerStateMachine}에 연료를 공급하고있습니다..");
        GoldManager.Instance.SubtractGold(towerModel.towerInfo.fuelSupplyRequiredCost);
        yield return new WaitForSeconds(2f);
        towerController.towerStateMachine.currFuelCapacity = towerModel.maxFuelCapacity;
        towerController.towerStateMachine.isFuelShortage = false;
        ViewManager.Instance.towerView.UpdateFuelSupplyCostText();
        yield break;
    }

    /// <summary>
    /// 파워 수리
    /// </summary>
    public void TryRepairPower(TowerController towerController, TowerModel towerModel)
    {
        StartCoroutine(RepairPower(towerController, towerModel));
    }

    public void TryAllRepairPower()
    {
        foreach(TowerController tower in towerList)
        {
            if(tower.towerStateMachine.isPowerDown)
            {
                StartCoroutine(RepairPower(tower, tower.towerModel));
            }
        }
    }

    public IEnumerator RepairPower(TowerController towerController, TowerModel towerModel)
    {
        if(GoldManager.Instance.CurrGold < towerModel.towerInfo.repairPowerRequiredCost) yield break;

        Debug.Log($"{towerController.towerStateMachine}에 파워를 수리하고있습니다..");
        GoldManager.Instance.SubtractGold(towerModel.towerInfo.repairPowerRequiredCost);
        yield return new WaitForSeconds(2f);
        towerController.towerStateMachine.isPowerDown = false;
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
