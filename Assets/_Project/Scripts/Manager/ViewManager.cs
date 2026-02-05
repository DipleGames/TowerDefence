using UnityEngine;

public class ViewManager : SingleTon<ViewManager>
{
    [Header("뷰 리스트")]
    public TowerView towerView;
    public MiningRigView miningRigView;
}
