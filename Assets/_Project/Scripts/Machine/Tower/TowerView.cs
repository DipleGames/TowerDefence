using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;

public class TowerView : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject towerStatePanel;

    [Header("수리 비용 Text")]
    public TextMeshProUGUI fuelSupplyCostText;
    public TextMeshProUGUI repairPowerCostText;

    [Header("상태 관련")]
    public Text possibilityOfPowerDownText;
    public Text towerRemainFuelText;
    public Text towerPowerText;


    [Header("스텟 관련")]
    public Text towerAttackPowerText;
    public Text towerAttackSpeedText;

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

    public void UpdatePossibilityOfPowerDownText(TowerStateMachine towerStateMachine)
    {
        possibilityOfPowerDownText.text = $"전력 차단 확률 : {towerStateMachine.possibilityOfPowerDown}%";
    }
    #endregion

    #region 스탯 관련 메서드
    public void UpdateTowerStatText(TowerStateMachine towerStateMachine)
    {
        towerAttackPowerText.text = $"공격력 : {towerStateMachine.towerController.towerModel.attackPower}";
        towerAttackSpeedText.text = $"공격속도 : {towerStateMachine.towerController.towerModel.attackSpeed}";
    }
    #endregion

    #region 수리 버튼 관련 메서드
    public void UpdateFuelSupplyCostText()
    {
        int cost = 0;
        foreach(TowerStateMachine tower in TowerManager.Instance.towerList)
        {
            if(tower.isFuelShortage)
            {
                cost += tower.towerController.towerModel.fuelSupplyRequiredCost;
            }
        }
        fuelSupplyCostText.text = $"{cost}";

    }

    public void UpdateRepairPowerCostText()
    {
        int cost = 0;
        foreach(TowerStateMachine tower in TowerManager.Instance.towerList)
        {
            if(tower.isPowerDown)
            {
                cost += tower.towerController.towerModel.repairPowerRequiredCost;
            }
        }
        repairPowerCostText.text = $"{cost}";
    }
    #endregion
}
