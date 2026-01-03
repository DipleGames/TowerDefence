using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : SingleTon<HUDManager>
{
    [Header("그리드 뷰 체인지 버튼")]
    public GameObject viewChangeBtn;

    [Header("골드 현황 텍스트")]
    public Text currGoldText;
    
    // 뷰 체인지 버튼 클릭 시 호출
    public void OnChangeViewChangeBtnText(GirdView gridView)
    {
        switch(gridView)
        {
            case GirdView.Main:
                viewChangeBtn.GetComponentInChildren<Text>().text = "채굴장";
                break;
            case GirdView.Mining:
                viewChangeBtn.GetComponentInChildren<Text>().text = "메인";
                break;
        }
    }

    // 현재 골드가 바뀔 시 호출
    public void OnChangeCurrentGoldText(int currGold)
    {
        currGoldText.text = $"{currGold}";
    }
}
