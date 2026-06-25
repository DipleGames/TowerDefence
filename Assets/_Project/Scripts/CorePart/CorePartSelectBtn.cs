using UnityEngine;
using UnityEngine.UI;

public class CorePartSelectBtn : MonoBehaviour
{
    public CorePart currentCorePart;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickedCorePartsSelectBtn);
    }

    public void SetCorePartsSelectBtn(CorePart coreParts)
    {
        currentCorePart = coreParts;        
    }

    void OnClickedCorePartsSelectBtn()
    {
        if(currentCorePart == null)
            return;

        CorePartsManager.Instance.ownedCorePartList.Add(currentCorePart);

        ViewManager.Instance.corePartsView.corePartPanel.SetActive(false);
        CorePartsManager.Instance.SetOwnedCorePartsList();
        GameManager.Instance.SetPause(false);
    }
}
