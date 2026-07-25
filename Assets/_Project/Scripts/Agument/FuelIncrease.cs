using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "FuelIncrease", menuName = "Data/Augment/Fuel Increase")]
public class FuelIncrease : AugmentData
{
    [Range(0f, 1f)] public float increaseRate = 0.1f;
    public override bool IsUnique => false;
    public override int Count { get; set; }    
    public override IEnumerator Execute()
    {
        TowerManager.Instance.AddFuelIncreaseBonus(increaseRate);

        foreach (var t in TowerManager.Instance.towerList)
        {
            t.towerStatCalculator.RecalculateStats(t);
        }
        yield break;
    }
}
