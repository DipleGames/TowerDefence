using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "GlacialAugment", menuName = "Data/Augment/Glacial Augment")]
public class GlacialAugment : AugmentData
{
    public override IEnumerator Execute()
    {
        TowerManager.Instance.ApplyGlacialAugment();
        yield break;
    }
}
