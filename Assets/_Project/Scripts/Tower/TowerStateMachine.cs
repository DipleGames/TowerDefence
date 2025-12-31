using UnityEngine;

public class TowerStateMachine : MonoBehaviour
{
    public enum TowerState { Active, InActive }

    public TowerState towerState = TowerState.Active;

    void Update()
    {
        switch(towerState)
        {
            case TowerState.Active:
                UpdateActiveState();
                break;
            case TowerState.InActive:
                break;
        }
    }

    void UpdateActiveState()
    {
        
    }
}
