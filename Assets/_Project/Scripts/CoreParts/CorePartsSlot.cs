using UnityEngine;
using UnityEngine.UI;

public class CorePartsSlot : MonoBehaviour
{
    [Header("슬롯에 장착된 코어파츠")]
    public CoreParts currentCoreParts;

    [Header("코어파츠 아이콘")]
    public Image corePartsIcon;

    public void SetCorePartsSlot(CoreParts coreParts)
    {
        currentCoreParts = coreParts;
    }

    public void ReleaseCorePartsSlot()
    {
        currentCoreParts = null;
        corePartsIcon.sprite = null;
        corePartsIcon.gameObject.SetActive(false);
    }
}
