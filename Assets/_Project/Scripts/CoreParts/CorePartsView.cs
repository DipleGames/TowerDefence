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
}
