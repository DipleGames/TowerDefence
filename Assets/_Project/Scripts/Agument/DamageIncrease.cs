using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageIncrease", menuName = "Data/Augment/Damage Increase")]
public class DamageIncrease : AugmentData
{
    [Range(0f, 1f)] public float increaseRate = 0.1f;

    public override IEnumerator Execute()
    {
        foreach (var t in TowerManager.Instance.towerList)
        {
            var model = t.towerController.towerModel;
            model.attackDamage += model.attackDamage * increaseRate;
        }
        yield break;
    }
}