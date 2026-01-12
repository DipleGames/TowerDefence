using TMPro;
using UnityEngine;

public class TowerRepairPowerButton : TowerRepairButton
{
    protected override void OnEnable()
    {
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{GetComponentInParent<TowerModel>().repairPowerRequiredCost}";    
    }

    public override void OnClickedRepairButton()
    {
        Debug.Log("클릭");
        TowerManager.Instance.TryRepairPower(GetComponentInParent<TowerStateMachine>(), GetComponentInParent<TowerModel>());
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
