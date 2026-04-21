using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "ReducedPowerOutageChance", menuName = "Data/Augment/Reduced Power Outage Chance")]
public class ReducedPowerOutageChance : AugmentData
{
    [Range(0f, 1f)] public float increaseRate = 0.1f;

    public override IEnumerator Execute()
    {
        TowerManager.Instance.AddReducedPowerOutageChanceRateBonus(increaseRate);

        foreach (var t in TowerManager.Instance.towerList)
        {
            t.towerStatCalculator.RecalculateStats(t);
        }
        yield break;
    }
}
