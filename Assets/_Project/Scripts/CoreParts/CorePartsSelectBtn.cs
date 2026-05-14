using UnityEngine;
using UnityEngine.UI;

public class CorePartsSelectBtn : MonoBehaviour
{
    public CoreParts currentCoreParts;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickedCorePartsSelectBtn);
    }

    public void SetCorePartsSelectBtn(CoreParts coreParts)
    {
        currentCoreParts = coreParts;        
    }

    void OnClickedCorePartsSelectBtn()
    {
        if(currentCoreParts == null)
            return;

        CorePartsManager.Instance.ownedCorePartsList.Add(currentCoreParts);

        ViewManager.Instance.corePartsView.corePartsPanel.SetActive(false);
        GameManager.Instance.SetPause(false);
    }
}
