using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : SingleTon<HUDManager>
{
    [Header("그리드 뷰 체인지 버튼")]
    public GameObject viewChangeBtn;

    [Header("골드 현황 텍스트")]
    public Text currGoldText;

    [Header("게임 진행 관련 UI")]
    public Text gameStateText;
    public Text gameStateTimeText;
    public GameObject skipBtn;

    [Header("옵션 UI")]
    public Button optionBtn;
    public GameObject optionPanel;


    [Header("타워 정보")]
    public GameObject towerStatePaenl;

    public void SwitchHUD(GameObject ui, bool b)
    {
        ui.SetActive(b);
    }

    public void OnClickedOptionBtn()
    {
        SwitchHUD(optionPanel, !optionPanel.activeSelf);
    }

    // 뷰 체인지 버튼 클릭 시 호출
    public void OnChangeViewChangeBtnText(GirdView gridView)
    {
        switch(gridView)
        {
            case GirdView.Main:
                viewChangeBtn.GetComponentInChildren<Text>().text = "채굴장";
                ViewManager.Instance.miningRigView.miningPanel.SetActive(false);
                ViewManager.Instance.towerView.mainPanel.SetActive(true);
                break;
            case GirdView.Mining:
                viewChangeBtn.GetComponentInChildren<Text>().text = "메인";
                ViewManager.Instance.miningRigView.miningPanel.SetActive(true);
                ViewManager.Instance.towerView.mainPanel.SetActive(false);
                break;
        }
    }

    // 현재 골드가 바뀔 시 호출
    public void OnChangeCurrentGoldText(int currGold)
    {
        currGoldText.text = $"{currGold}";
    }

    // 게임 상태가 바뀔 시 호출
    public void OnChangeGameState(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.Wave:
                gameStateText.text = $"{gameState} {GameManager.Instance.currWave}";
                skipBtn.SetActive(false);
                break;
            case GameState.Prepare:
                gameStateText.text = $"{gameState}";
                skipBtn.SetActive(true);
                break;
        }
    }

    private WaitForSeconds wait01 = new WaitForSeconds(0.1f);
    public IEnumerator StartTimer(float duration)
    {
        float startTime = Time.time; // 시작한 시점의 시간 기록
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // 실제 경과 시간 계산 (프레임 밀림 방지)
            elapsed = Time.time - startTime;
            float remainingTime = Mathf.Max(0, duration - elapsed);

            // UI 갱신
            gameStateTimeText.text = remainingTime.ToString("F1");

            yield return wait01;
        }

        gameStateTimeText.text = "0.0";
    }
}
