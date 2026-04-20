using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FieldGridManager : SingleTon<FieldGridManager>
{
    [SerializeField] private List<FieldNode> fieldNodes = new();
    [SerializeField] private Tilemap fieldTilemap;

    private Dictionary<FieldNode, TowerStateMachine> _placedTowers = new();

    protected override void Awake()
    {
        base.Awake();
        for(int i=0; i<fieldTilemap.transform.childCount; i++)
        {
            fieldNodes.Add(fieldTilemap.transform.GetChild(i).transform.GetComponent<FieldNode>());
        }
    }

    void Start()
    {
        RegisterExistingTowers(TowerManager.Instance.towerList);
    }


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
        node.SetTower(tower);

        MachineGridObject towerGridObject = tower.GetComponent<MachineGridObject>();
        if (towerGridObject != null)
        {
            towerGridObject.SetCurrentFieldNode(node);
        }
    }

    public void RemoveTower(FieldNode node)
    {
        if (_placedTowers.TryGetValue(node, out TowerStateMachine tower))
        {
            MachineGridObject towerGridObject = tower.GetComponent<MachineGridObject>();
            if (towerGridObject != null)
            {
                towerGridObject.SetCurrentFieldNode(null);
            }
        }

        if (_placedTowers.ContainsKey(node))
            _placedTowers.Remove(node);

        node.ClearTower();
    }

    public Vector3 GetNodeCenterWorld(FieldNode node)
    {
        return node.transform.position;
    }

    public FieldNode FindFieldNodeUnderTower(TowerStateMachine tower)
    {
        Vector3 origin = tower.transform.position + Vector3.up * 2f;

        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, 10f))
        {
            FieldNode node = hit.collider.GetComponent<FieldNode>();
            return node;
        }

        return null;
    }

    public void RegisterExistingTowers(List<TowerStateMachine> towerList)
    {
        foreach (TowerStateMachine tower in towerList)
        {
            FieldNode node = FindFieldNodeUnderTower(tower);

            if (node != null)
            {
                SetTower(node, tower);
                Debug.Log($"{tower} 아래에 {node}를 찾음");
            }
            else
            {
                Debug.LogWarning($"{tower.name} 아래에 FieldNode를 찾지 못함");
            }
        }
    }
}