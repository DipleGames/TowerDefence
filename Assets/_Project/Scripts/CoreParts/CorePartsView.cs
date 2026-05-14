using System.Collections.Generic;
using UnityEngine;

public class CorePartsView : MonoBehaviour
{
    [Header("코어파츠 뽑기 패널")]
    public GameObject corePartsPanel;

    [Header("코어파츠 버튼 리스트")]
    public List<CorePartsSelectBtn> corePartsSelectBtnList = new();

    public void SetCorePartsBtn(List<CoreParts> corePartsList)
    {
        for(int i=0; i<3; i++)
        {
            corePartsSelectBtnList[i].currentCoreParts = corePartsList[i];
        }
    }
}
