using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorePartsManager : SingleTon<CorePartsManager>
{

    [Header("코어파츠 리스트")]
    public List<CoreParts> corePartsList = new();

    [Header("보유하고있는 코어파츠 리스트")]
    public List<CoreParts> ownedCorePartsList = new();


    HashSet<int> ranHash = new();
    List<CoreParts> currentCorePartsList = new();
    public void OnClickedCorePartsDrawBtn()
    {
        bool isActive = ViewManager.Instance.corePartsView.corePartsPanel.activeSelf;
        ViewManager.Instance.corePartsView.corePartsPanel.SetActive(!isActive);

        GameManager.Instance.SetPause(!isActive);

        ranHash.Clear();
        currentCorePartsList.Clear();
        while(ranHash.Count < 3)
        {
            int ran = Random.Range(0, corePartsList.Count);
            ranHash.Add(ran);
        }

        foreach(int ran in ranHash)
        {
            currentCorePartsList.Add(corePartsList[ran]);
        }

        SetCorePartsBtn(currentCorePartsList);
    }

    public void SetCorePartsBtn(List<CoreParts> corePartsList)
    {
        for(int i=0; i<3; i++)
        {
            ViewManager.Instance.corePartsView.corePartsSelectBtnList[i].currentCoreParts = corePartsList[i];
        }
    }

    public void SetOwnedCorePartsList()
    {
        for (int i = 0; i < ownedCorePartsList.Count; i++)
        {
            if(ownedCorePartsList.Count > 6)
            {
                Debug.Log("다음페이지");
                return;
            }
            Transform slot = ViewManager.Instance.corePartsView.ownedCorePartsListPanel.transform.GetChild(i);

            CorePartsSlot corePartsSlot = slot.GetComponent<CorePartsSlot>();

            corePartsSlot.SetCorePartsSlot(ownedCorePartsList[i]);
            corePartsSlot.corePartsIcon.sprite = corePartsSlot.currentCoreParts.coreImg;
            if(corePartsSlot.corePartsIcon != null)
            { 
                corePartsSlot.corePartsIcon.gameObject.SetActive(true);
            }
        }
    }
}
