using System;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : SingleTon<GoldManager>
{
    [SerializeField] private int _currGold;
    public event Action<int> OnChangedGold;

    protected override void Awake()
    {
        base.Awake();
        OnChangedGold += HUDManager.Instance.OnChangeCurrentGoldText;
    }

    public int CurrGold
    {
        get => _currGold;
        set
        {
            int nv = value;
            _currGold = nv;
        }
    }

    public void AddGold(int amount)
    {
        CurrGold += amount;
        OnChangedGold.Invoke(CurrGold);
    }

    public void SubtractGodld(int amount)
    {
        CurrGold -= amount;
        OnChangedGold.Invoke(CurrGold);
    }
}
