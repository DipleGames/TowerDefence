using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour, IDamageable
{
    [SerializeField] private Slider _slider;
    public MonsterModel monsterModel;

    void Awake()
    {
        InitMonster();
        ViewManager.Instance.monsterView.UpdateHPBar(monsterModel, _slider);
    }

    public void InitMonster()
    {
        monsterModel.CurrentHP = monsterModel.maxHP;
    }



    public void TakeDamage(float amount)
    {
        monsterModel.CurrentHP -= amount;
        ViewManager.Instance.monsterView.UpdateHPBar(monsterModel, _slider);
    }
}
