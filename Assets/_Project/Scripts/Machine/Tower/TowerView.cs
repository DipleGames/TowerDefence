using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerView : MonoBehaviour
{
    public GameObject towerStatePanel;
    public Text towerNameText;
    public Text towerRemainFuelText;

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

        while (true)
        {
            if(towerStateMachine.towerState == MachineState.Active)
            {
                float remainTime = towerStateMachine.maxActiveTime - towerStateMachine.activeTime;
                float percent = (remainTime / towerStateMachine.maxActiveTime) * 100f;

                // 0.1% 단위로 내림
                float displayPercent = Mathf.Floor(percent * 10f) / 10f;

                if (!Mathf.Approximately(displayPercent, lastShownPercent))
                {
                    towerRemainFuelText.text = $"남은 연료 : {displayPercent:0.0}%";
                    lastShownPercent = displayPercent;
                }
            }
            else if(towerStateMachine.towerState == MachineState.InActive)
            {
                towerRemainFuelText.text = "연료 부족";
            }

            yield return null; // 매 프레임 체크
        }
    }

    #endregion
}
