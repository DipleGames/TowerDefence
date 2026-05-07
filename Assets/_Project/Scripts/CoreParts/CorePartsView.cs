using System.Collections.Generic;
using UnityEngine;

public class CorePartsView : MonoBehaviour
{
    [Header("코어파츠 뽑기 패널")]
    public GameObject corePartsPanel;

    [Header("코어파츠 선택 리스트")]
    public List<GameObject> corePartsList = new();
}
