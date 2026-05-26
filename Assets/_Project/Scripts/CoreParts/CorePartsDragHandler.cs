using UnityEngine;
using UnityEngine.EventSystems;

public class CorePartsDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    private Vector3 startPos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = rectTransform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position += (Vector3)eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            TowerModel tower = hit.collider.GetComponentInParent<TowerModel>();

            if (tower != null)
            {
                EquipCorePart(tower);
                // 장착 처리
                // tower.EquipCorePart(...);
            }
        }

        rectTransform.position = startPos;
    }

    void EquipCorePart(TowerModel tower)
    {
        Debug.Log("타워에 코어파츠 장착!");
        CorePartsSlot corePartsSlot = GetComponentInParent<CorePartsSlot>();
        AudioManager.Instance.PlaySFX(corePartsSlot.currentCoreParts.equipSFX);
        tower.equippedPartsList.Add(corePartsSlot.currentCoreParts);
        CorePartsManager.Instance.ownedCorePartsList.Remove(corePartsSlot.currentCoreParts);
        corePartsSlot.ReleaseCorePartsSlot();
    }
}