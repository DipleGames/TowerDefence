using UnityEngine;
using UnityEngine.UI;

public class CorePartSlot : MonoBehaviour
{
    [Header("슬롯에 장착된 코어파츠")]
    public CorePart currentCorePart;

    [Header("코어파츠 아이콘")]
    public Image corePartsIcon;

    public void SetCorePartsSlot(CorePart coreParts)
    {
        currentCorePart = coreParts;
    }

    public void ReleaseCorePartsSlot()
    {
        currentCorePart = null;
        corePartsIcon.sprite = null;
        corePartsIcon.gameObject.SetActive(false);
    }
}
