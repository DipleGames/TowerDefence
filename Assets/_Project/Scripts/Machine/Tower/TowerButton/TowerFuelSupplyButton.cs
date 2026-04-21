using UnityEngine;
using TMPro;

public class TowerFuelSupplyButton : TowerRepairButton
{
    protected override void OnEnable()
    {
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{GetComponentInParent<TowerModel>().fuelSupplyRequiredCost}";    
    }

    public override void OnClickedRepairButton()
    {
        Debug.Log("클릭");
        TowerManager.Instance.TryFuelSupply(GetComponentInParent<TowerController>(), GetComponentInParent<TowerModel>());
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
