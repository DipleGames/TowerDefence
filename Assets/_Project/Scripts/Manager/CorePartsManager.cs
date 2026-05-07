using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorePartsManager : MonoBehaviour
{
    [Header("코어파츠 리스트")]
    public List<GameObject> corePartsList = new();


    public void OnClickedCorePartsDrawBtn()
    {
        bool isActive = ViewManager.Instance.corePartsView.corePartsPanel.activeSelf;
        ViewManager.Instance.corePartsView.corePartsPanel.SetActive(!isActive);

        GameManager.Instance.SetPause(!isActive);
    }


}
