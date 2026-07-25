using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackSpeedIncrease", menuName = "Data/Augment/AttackSpeed Increase")]
public class AttackSpeedIncrease : AugmentData
{
    [Range(0f, 1f)] public float increaseRate = 0.9f;
    public override bool IsUnique => false;
    public override int Count { get; set; }

    public override IEnumerator Execute()
    {

        TowerManager.Instance.AddAttackSpeedBonus(increaseRate);

        foreach (var t in TowerManager.Instance.towerList)
        {
            t.towerStatCalculator.RecalculateStats(t);
        }
        yield break;
    }
}
