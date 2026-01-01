using UnityEngine;

public class MonsterModel : MonoBehaviour
{
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
            if(_currentHP <= 0f) Destroy(gameObject);
        }
    }
}
