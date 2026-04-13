using UnityEngine;
using UnityEngine.UI;

public class MonsterView : MonoBehaviour
{

    public void UpdateHPBar(MonsterModel monsterModel, Slider slider)
    {
        slider.value = monsterModel.CurrentHP / monsterModel.maxHP;
    }
}
