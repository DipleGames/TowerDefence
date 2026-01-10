using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : SingleTon<HUDManager>
{
    [Header("그리드 뷰 체인지 버튼")]
    public GameObject viewChangeBtn;

    [Header("골드 현황 텍스트")]
    public Text currGoldText;

    [Header("게임 현황 텍스트")]
    public Text gameStateText;
    public Text gameStateTimeText;


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

    // 게임 상태가 바뀔 시 호출
    public void OnChangeGameStateText(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.Wave:
                gameStateText.text = $"{gameState} {GameManager.Instance.currWave}";
                break;
            case GameState.Prepare:
                gameStateText.text = $"{gameState}";
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
