using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TowerView : MonoBehaviour
{
    public GameObject towerStatePanel;
    public Text towerNameText;
    public Text towerRemainFuelText;
    public Text towerPowerText;

    public void OnUpdateTowerNameText(string towerName)
    {
        towerNameText.text = towerName;
    }

    #region 연료 관련 메서드
    Coroutine fuelCoroutine;
    public void StartFuelView(TowerStateMachine tower)
    {
        StopFuelView();
        fuelCoroutine = StartCoroutine(OnUpdateRemainingFuelCoroutine(tower));
    }

    public void StopFuelView()
    {
        if (fuelCoroutine != null)
        {
            StopCoroutine(fuelCoroutine);
            fuelCoroutine = null;
        }
    }

    public IEnumerator OnUpdateRemainingFuelCoroutine(TowerStateMachine towerStateMachine)
    {
        float lastShownPercent = -1f;

        while (!towerStateMachine.isFuelShortage)
        {
            float remainTime = towerStateMachine.currFuelCapacity;
            float percent = (remainTime / towerStateMachine.maxFuelCapacity) * 100f;

            // 0.1% 단위로 내림
            float displayPercent = Mathf.Floor(percent * 10f) / 10f;

            if (!Mathf.Approximately(displayPercent, lastShownPercent))
            {
                towerRemainFuelText.text = $"남은 연료 : {displayPercent:0.0}%";
                lastShownPercent = displayPercent;
            }

            yield return null; // 매 프레임 체크
        }
        towerRemainFuelText.text = "연료 부족";
        yield break;
    }
    #endregion

    #region 파워 관련 메서드
    void OnUpdatePowerText(TowerStateMachine towerStateMachine)
    {
        towerPowerText.text = towerStateMachine.isPowerDown ? "파워 상태 : OFF" : "파워 상태 : ON" ;  
    }

    Coroutine powerCoroutine;
    public void StartPowerView(TowerStateMachine tower)
    {
        StopPowerView();
        powerCoroutine = StartCoroutine(OnUpdatePowerCoroutine(tower));
    }

    public void StopPowerView()
    {
        if (powerCoroutine != null)
        {
            StopCoroutine(powerCoroutine);
            powerCoroutine = null;
        }
    }

    public IEnumerator OnUpdatePowerCoroutine(TowerStateMachine towerStateMachine)
    {
        while (true)
        {
            OnUpdatePowerText(towerStateMachine);

            yield return new WaitForSeconds(0.1f);
        }
    }
    #endregion
}
