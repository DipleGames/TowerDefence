using TMPro;
using UnityEngine;

public class TowerRepairButton : MonoBehaviour
{
    void OnEnable()
    {
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{GetComponentInParent<TowerModel>().repairRequiredCost}";    
    }

    public void OnClickRepairButton()
    {
        Debug.Log("클릭");
        TowerManager.Instance.TryRepair(GetComponentInParent<TowerStateMachine>(), GetComponentInParent<TowerModel>());
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
