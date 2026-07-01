using UnityEngine;

public class TowerStatCalculator : MonoBehaviour
{
    public void RecalculateStats(TowerController towerController)
    {
        towerController.towerModel.attackPower = towerController.towerModel.towerInfo.baseAttackPower * (1f + TowerManager.Instance.AttackPowerRateBonus);
        towerController.towerModel.attackSpeed = towerController.towerModel.towerInfo.baseAttackSpeed * TowerManager.Instance.AttackSpeedMultiplier;
        towerController.towerModel.criticalProb = towerController.towerModel.towerInfo.baseCriticalProb + TowerManager.Instance.CriticalProbBonus;
        towerController.towerModel.maxFuelCapacity = towerController.towerModel.towerInfo.baseMaxFuelCapacity * (1f + TowerManager.Instance.FuelIncreaseRateBonus);
        towerController.towerStateMachine.currPossibilityOfPowerDown = towerController.towerStateMachine.towerController.towerModel.towerInfo.baseReducedPowerOutageChance - TowerManager.Instance.ReducedPowerOutageChanceRateBonus;
    }
}
