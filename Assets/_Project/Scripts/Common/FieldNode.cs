using UnityEngine;

public class FieldNode : MonoBehaviour
{
    [SerializeField] private GameObject highlightObject;
    public Vector3 WorldPosition => transform.position;

    public TowerController CurrentTower { get; private set; }

    public bool HasTower => CurrentTower != null;

    public void SetTower(TowerController tower)
    {
        CurrentTower = tower;
    }

    public void ClearTower()
    {
        CurrentTower = null;
    }

    public void ShowHighlight(bool isShow)
    {
        if (highlightObject != null)
            highlightObject.SetActive(isShow);
    }

    
}