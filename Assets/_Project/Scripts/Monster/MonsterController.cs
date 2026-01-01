using UnityEngine;

public class MonsterController : MonoBehaviour, IDamageable
{
    public MonsterModel monsterModel;

    void Awake()
    {
        InitMonster();
    }

    void InitMonster()
    {
        monsterModel.CurrentHP = monsterModel.maxHP;
    }
    public void TakeDamage(float amount)
    {
        monsterModel.CurrentHP -= amount;
    }
}
