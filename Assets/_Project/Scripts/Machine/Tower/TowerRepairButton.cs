using UnityEngine;

public class TowerRepairButton : MonoBehaviour
{
    public void OnClickRepairButton()
    {
        Debug.Log("클릭");
        TowerManager.Instance.TryRepair(GetComponentInParent<TowerStateMachine>());
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
