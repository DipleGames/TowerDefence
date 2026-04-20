using UnityEngine;

public class TowerStatCalculator : MonoBehaviour
{
    public void RecalculateStats(TowerStateMachine towerStateMachine)
    {
        towerStateMachine.towerController.towerModel.attackPower = towerStateMachine.towerController.towerModel.baseAttackPower * (1f + TowerManager.Instance.AttackPowerRateBonus);
        towerStateMachine.towerController.towerModel.attackSpeed = towerStateMachine.towerController.towerModel.baseAttackSpeed * TowerManager.Instance.AttackSpeedMultiplier;
        towerStateMachine.towerController.towerModel.maxFuelCapacity = towerStateMachine.towerController.towerModel.baseMaxFuelCapacity * (1f + TowerManager.Instance.FuelIncreaseRateBonus);
        towerStateMachine.currPossibilityOfPowerDown = towerStateMachine.towerController.towerModel.baseReducedPowerOutageChance - TowerManager.Instance.ReducedPowerOutageChanceRateBonus;
    }
}
