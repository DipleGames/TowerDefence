using System.Collections.Generic;
using UnityEngine;

public class FieldGridManager : SingleTon<FieldGridManager>
{
    [SerializeField] private List<FieldNode> fieldNodes = new();

    private Dictionary<FieldNode, TowerStateMachine> _placedTowers = new();

  
    public bool IsBuildableNode(FieldNode node)
    {
        return node != null;
    }

    public bool HasTower(FieldNode node)
    {
        return _placedTowers.ContainsKey(node);
    }

    public TowerStateMachine GetTower(FieldNode node)
    {
        _placedTowers.TryGetValue(node, out TowerStateMachine tower);
        return tower;
    }

    public void SetTower(FieldNode node, TowerStateMachine tower)
    {
        _placedTowers[node] = tower;
    }

    public void RemoveTower(FieldNode node)
    {
        if (_placedTowers.ContainsKey(node))
            _placedTowers.Remove(node);
    }

    public Vector3 GetNodeCenterWorld(FieldNode node)
    {
        return node.transform.position;
    }
}