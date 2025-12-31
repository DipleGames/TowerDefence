using UnityEngine;

public class MonsterController : MonoBehaviour, IDamageable
{
    public MonsterModel monsterModel;

    public void TakeDamage(float amount)
    {
        monsterModel.CurrentHP -= amount;
    }
}
