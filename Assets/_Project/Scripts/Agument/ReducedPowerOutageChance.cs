using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ReducedPowerOutageChance", menuName = "Data/Augment/Reduced Power Outage Chance")]
public class ReducedPowerOutageChance : AugmentData
{
    [Range(0f, 1f)] public float increaseRate = 0.1f;

    public override IEnumerator Execute()
    {
        foreach (var t in TowerManager.Instance.towerList)
        {
            var towerStateMachine = t.towerController.towerStateMachine;      
            towerStateMachine.possibilityOfPowerDown -= 0.1f; // 확률 0.1% 감소
        }
        yield break;
    }
}
