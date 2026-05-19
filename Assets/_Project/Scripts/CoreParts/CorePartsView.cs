using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CorePartsView : MonoBehaviour
{
    [Header("코어파츠 뽑기 패널")]
    public GameObject corePartsPanel;

    [Header("코어파츠 버튼 리스트")]
    public List<CorePartsSelectBtn> corePartsSelectBtnList = new();

    [Header("보유하고있는 코어파츠 리스트 UI")]
    public GameObject ownedCorePartsListPanel;

    public void SetCorePartsBtn(List<CoreParts> corePartsList)
    {
        for(int i=0; i<3; i++)
        {
            corePartsSelectBtnList[i].currentCoreParts = corePartsList[i];
        }
    }

    public void SetOwnedCorePartsList()
    {
        for (int i = 0; i < CorePartsManager.Instance.ownedCorePartsList.Count; i++)
        {
            if(CorePartsManager.Instance.ownedCorePartsList.Count > 6)
            {
                Debug.Log("다음페이지");
                return;
            }
            Transform slot = ownedCorePartsListPanel.transform.GetChild(i);

            Image iconImage = slot.Find("Icon").GetComponent<Image>();

            iconImage.sprite = CorePartsManager.Instance.ownedCorePartsList[i].coreImg;
            if(iconImage != null)
            { 
                iconImage.gameObject.SetActive(true);
            }
        }
    }
}
