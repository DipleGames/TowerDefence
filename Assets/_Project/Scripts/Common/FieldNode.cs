using UnityEngine;

public class FieldNode : MonoBehaviour
{
    public Vector3 WorldPosition => transform.position;

    public TowerStateMachine CurrentTower { get; private set; }

    public bool HasTower => CurrentTower != null;

    public void SetTower(TowerStateMachine tower)
    {
        CurrentTower = tower;
    }

    public void ClearTower()
    {
        CurrentTower = null;
    }
}