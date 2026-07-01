using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Critical", menuName = "Data/Augment/Critical")]
public class Critical : AugmentData
{
    [SerializeField] private float criticalProb = 0.1f;

    public override IEnumerator Execute()
    {
        TowerManager.Instance.AddCriticalProbBonus(criticalProb);

        foreach (var t in TowerManager.Instance.towerList)
        {
            t.towerStatCalculator.RecalculateStats(t);
        }
        yield break;
    }
}
