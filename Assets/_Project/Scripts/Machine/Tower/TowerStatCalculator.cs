using UnityEngine;

public class TowerStatCalculator : MonoBehaviour
{
    public void RecalculateStats(TowerController towerController)
    {
        towerController.towerModel.attackPower = towerController.towerModel.baseAttackPower * (1f + TowerManager.Instance.AttackPowerRateBonus);
        towerController.towerModel.attackSpeed = towerController.towerModel.baseAttackSpeed * TowerManager.Instance.AttackSpeedMultiplier;
        towerController.towerModel.maxFuelCapacity = towerController.towerModel.baseMaxFuelCapacity * (1f + TowerManager.Instance.FuelIncreaseRateBonus);
        towerController.towerStateMachine.currPossibilityOfPowerDown = towerController.towerStateMachine.towerController.towerModel.baseReducedPowerOutageChance - TowerManager.Instance.ReducedPowerOutageChanceRateBonus;
    }
}
