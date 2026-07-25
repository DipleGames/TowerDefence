using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CorePartsManager : SingleTon<CorePartsManager>
{

    [Header("코어파츠 리스트(원본)")]
    public List<CorePart> originCorePartList = new();

    [Header("코어파츠 리스트(Clone)")]
    public List<CorePart> corePartList = new();

    [Header("보유하고있는 코어파츠 리스트")]
    public List<CorePart> ownedCorePartList = new();

    protected override void Awake()
    {
        foreach(var corePart in originCorePartList)
        {
            CorePart corePartInstance = Instantiate(corePart);
            corePartList.Add(corePartInstance);
        }
    }

    HashSet<int> ranHash = new();
    List<CorePart> currentCorePartList = new();
    public void OnClickedCorePartsDrawBtn()
    {
        bool isActive = ViewManager.Instance.corePartsView.corePartPanel.activeSelf;
        ViewManager.Instance.corePartsView.corePartPanel.SetActive(!isActive);

        GameManager.Instance.SetPause(!isActive);

        ranHash.Clear();
        currentCorePartList.Clear();
        while(ranHash.Count < 3)
        {
            int ran = Random.Range(0, corePartList.Count);
            ranHash.Add(ran);
        }

        foreach(int ran in ranHash)
        {
            currentCorePartList.Add(corePartList[ran]);
        }

        SetCorePartsBtn(currentCorePartList);
    }

    public void SetCorePartsBtn(List<CorePart> corePartsList)
    {
        for(int i=0; i<3; i++)
        {
            ViewManager.Instance.corePartsView.corePartSelectBtnList[i].currentCorePart = corePartsList[i];
            Image corePartImg = ViewManager.Instance.corePartsView.corePartSelectBtnList[i].gameObject.transform.GetChild(0).GetComponent<Image>();
            TextMeshProUGUI corePartDesc = ViewManager.Instance.corePartsView.corePartSelectBtnList[i].gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

            corePartImg.sprite = ViewManager.Instance.corePartsView.corePartSelectBtnList[i].currentCorePart.coreImg;
            corePartDesc.text = ViewManager.Instance.corePartsView.corePartSelectBtnList[i].currentCorePart.coreDescription;
        }
    }

    public void SetOwnedCorePartsList()
    {
        for (int i = 0; i < ownedCorePartList.Count; i++)
        {
            if(ownedCorePartList.Count > 6)
            {
                Debug.Log("다음페이지");
                return;
            }
            Transform slot = ViewManager.Instance.corePartsView.ownedCorePartsListPanel.transform.GetChild(i);

            CorePartSlot corePartsSlot = slot.GetComponent<CorePartSlot>();

            corePartsSlot.SetCorePartsSlot(ownedCorePartList[i]);
            corePartsSlot.corePartsIcon.sprite = corePartsSlot.currentCorePart.coreImg;
            if(corePartsSlot.corePartsIcon != null)
            { 
                corePartsSlot.corePartsIcon.gameObject.SetActive(true);
            }
        }
    }
}
