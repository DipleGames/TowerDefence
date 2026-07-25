using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "GlacialAugment", menuName = "Data/Augment/Glacial Augment")]
public class GlacialAugment : AugmentData
{
    public override bool IsUnique => true;
    public override int Count { get; set; }
    public override IEnumerator Execute()
    {
        TowerManager.Instance.ApplyGlacialAugment();
        yield break;
    }
}
