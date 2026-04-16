using UnityEngine;

public class TowerDragHandler : MonoBehaviour
{
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            FieldNode fieldNode = hit.collider.GetComponent<FieldNode>();

            if (fieldNode != null)
            {
                Debug.Log($"Field 감지: {fieldNode.name}, Pos: {fieldNode.transform.position}, 타워 여부 : {FieldGridManager.Instance.HasTower(fieldNode)}");
            }
        }
    }
}