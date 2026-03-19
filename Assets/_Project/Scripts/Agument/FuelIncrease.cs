using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "FuelIncrease", menuName = "Data/Augment/Fuel Increase")]
public class FuelIncrease : AugmentData
{
    [Range(0f, 1f)] public float increaseRate = 0.1f;

    public override IEnumerator Execute()
    {
        foreach (var t in TowerManager.Instance.towerList)
        {
            var towerStateMachine = t.towerController.towerStateMachine;
            float bouns = towerStateMachine.maxFuelCapacity * 0.1f;

            towerStateMachine.maxFuelCapacity += bouns; // 연료량 10% 증가
        }
        yield break;
    }
}
