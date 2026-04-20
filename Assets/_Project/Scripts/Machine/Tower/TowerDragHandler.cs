using UnityEngine;

public class TowerDragHandler : MonoBehaviour
{
    [SerializeField] private LayerMask fieldLayerMask;
    private MachineGridObject _gridObject;
    private TowerStateMachine _tower;

    private Vector3 _startPosition;
    private bool _isDragging;

    private void Awake()
    {
        _gridObject = GetComponent<MachineGridObject>();
        _tower = GetComponent<TowerStateMachine>();
    }

    private void OnMouseDown()
    {
        _startPosition = transform.position;
        _isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (!_isDragging) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane plane = new Plane(Vector3.up, Vector3.zero);

        if (plane.Raycast(ray, out float enter))
        {
            Vector3 pos = ray.GetPoint(enter);
            transform.position = new Vector3(pos.x, _startPosition.y, pos.z);
        }
    }

    private void OnMouseUp()
    {
        if (!_isDragging) return;

        _isDragging = false;

        TryDrop();
    }

    private void TryDrop()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, fieldLayerMask))
        {
            FieldNode targetNode = hit.collider.GetComponent<FieldNode>();

            if (targetNode == null)
            {
                ReturnToStart();
                Debug.Log("타겟노드 널");
                return;
            }

            HandleNodeInteraction(targetNode);
        }
        else
        {
            Debug.Log("Field를 안 맞음");
            ReturnToStart();
        }
    }

    private void HandleNodeInteraction(FieldNode targetNode)
    {
        FieldGridManager grid = FieldGridManager.Instance;
        FieldNode currentNode = _gridObject.CurrentFieldNode;

        // 같은 칸이면 아무것도 안함
        if (targetNode == currentNode)
        {
            ReturnToStart();
            return;
        }

        // 1. 빈 칸
        if (!grid.HasTower(targetNode))
        {
            MoveToNode(targetNode);
            return;
        }

        // 2. 타워 있음
        TowerStateMachine otherTower = grid.GetTower(targetNode);

        if (CanMerge(_tower, otherTower))
        {
            Merge(targetNode, otherTower);
        }
        else
        {
            Debug.Log("다른 타워라 배치 불가");
            ReturnToStart();
        }
    }

    private void MoveToNode(FieldNode node)
    {
        FieldGridManager grid = FieldGridManager.Instance;

        FieldNode oldNode = _gridObject.CurrentFieldNode;

        if (oldNode != null)
        {
            grid.RemoveTower(oldNode);
        }

        transform.position = node.transform.position;

        grid.SetTower(node, _tower);

        _gridObject.SetCurrentFieldNode(node);
    }

    private bool CanMerge(TowerStateMachine a, TowerStateMachine b)
    {
        // 지금은 간단히 "같은 이름"으로 판정
        return a.name == b.name;
    }

    private void Merge(FieldNode node, TowerStateMachine otherTower)
    {
        Debug.Log("합성!");

        FieldGridManager grid = FieldGridManager.Instance;

        FieldNode myNode = _gridObject.CurrentFieldNode;

        // 기존 제거
        if (myNode != null)
            grid.RemoveTower(myNode);

        grid.RemoveTower(node);

        Destroy(otherTower.gameObject);
        Destroy(gameObject);

        // 👉 여기서 다음 티어 타워 생성하면 됨 (다음 단계)
    }

    private void ReturnToStart()
    {
        transform.position = _startPosition;
    }
}