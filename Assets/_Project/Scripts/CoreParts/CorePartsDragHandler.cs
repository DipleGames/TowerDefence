using UnityEngine;
using UnityEngine.EventSystems;

public class CorePartsDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private CorePartsSlot _corePartsSlot;

    private Vector3 startPos;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _corePartsSlot = GetComponentInParent<CorePartsSlot>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = _rectTransform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.position += (Vector3)eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            TowerModel tower = hit.collider.GetComponentInParent<TowerModel>();

            if (tower != null)
            {
                tower.EquipCorePart(_corePartsSlot.currentCoreParts);
                _corePartsSlot.ReleaseCorePartsSlot();
                // 장착 처리
                // tower.EquipCorePart(...);
            }
        }

        _rectTransform.position = startPos;
    }
}