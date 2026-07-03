using UnityEngine;
using UnityEngine.AI;

public class MonsterModel : MonoBehaviour
{
    [Header("몬스터 ID")]
    public int monsterID;
    
    [Header("체력")]
    [SerializeField] private float _currentHP;
    public float CurrentHP
    {
        get => _currentHP;
        set
        {
            _currentHP = value;
            if(_currentHP <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    [Header("방어력")]
    [SerializeField] private float _currentArmor;
    public float CurrentArmor
    {
        get => _currentArmor;
        set
        {
            float nv = value;
            if(Mathf.Approximately(_currentArmor, nv)) return;
            _currentArmor = nv;
        }
    }
}
