using UnityEngine;
using UnityEngine.AI;

public class MonsterModel : MonoBehaviour
{
    [Header("몬스터 ID")]
    public int monsterID;
    
    [Header("체력")]
    public float maxHP;
    [SerializeField] private float _currentHP;
    public float CurrentHP
    {
        get => _currentHP;
        set
        {
            float max = maxHP;
            float nv = Mathf.Clamp(value, 0f, max);
            if (Mathf.Approximately(_currentHP, nv)) return;
            _currentHP = nv;
            if(_currentHP <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    [Header("방어력")]
    public float maxArmor;
    [SerializeField] private float _currentArmor;
    public float CurrentArmor
    {
        get => _currentArmor;
        set
        {
            float max = maxArmor;
            float nv = Mathf.Clamp(value, 0f, max);
            if(Mathf.Approximately(_currentArmor, nv)) return;
            _currentArmor = nv;
        }
    }
}
