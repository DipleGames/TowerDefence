using UnityEngine;
using UnityEngine.EventSystems;

public class CorePartDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private CorePartSlot _corePartSlot;

    private Vector3 startPos;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _corePartSlot = GetComponentInParent<CorePartSlot>();
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
                tower.EquipCorePart(_corePartSlot.currentCorePart);
                _corePartSlot.ReleaseCorePartsSlot();
                // 장착 처리
                // tower.EquipCorePart(...);
            }
        }

        _rectTransform.position = startPos;
    }
}