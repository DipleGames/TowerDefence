using UnityEngine;

public abstract class TowerRepairButton : MonoBehaviour
{
    protected abstract void OnEnable();
    public abstract void OnClickedRepairButton();
}
