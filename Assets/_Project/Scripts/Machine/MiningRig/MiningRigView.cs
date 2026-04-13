using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiningRigView : MonoBehaviour
{
    [Header("MiningRig 패널")]
    public GameObject miningPanel;

    [Header("MiningRig Upgrade")]
    public Button expandFieldBtn;
    public Button addOnMiningRigBtn;
    public TextMeshProUGUI expandCostText;
    public TextMeshProUGUI additionCostText;

    public void UpdateExpandCostText(int cost)
    {
        expandCostText.text = $"{cost}";
    }

    public void UpdateAdditionCostText(int cost)
    {
        additionCostText.text = $"{cost}";
    }

}
