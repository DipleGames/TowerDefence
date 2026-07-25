using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageIncrease", menuName = "Data/Augment/Damage Increase")]
public class DamageIncrease : AugmentData
{
    [Range(0f, 1f)] public float increaseRate = 0.1f;
    public override bool IsUnique => false;
    public override int Count { get; set; }

    public override IEnumerator Execute()
    {
        TowerManager.Instance.AddAttackPowerBonus(increaseRate);

        foreach (var t in TowerManager.Instance.towerList)
        {
            t.towerStatCalculator.RecalculateStats(t);
        }
        yield break;
    }
}