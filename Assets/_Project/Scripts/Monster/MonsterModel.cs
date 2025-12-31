using UnityEngine;

public class MonsterModel : MonoBehaviour
{
    [SerializeField] private float _maxHP;
    [SerializeField] private float _currentHP;
    public float CurrentHP
    {
        get => _currentHP;
        set
        {
            float max = _maxHP;
            float nv = Mathf.Clamp(value, 0f, max);
            if (Mathf.Approximately(_currentHP, nv)) return;
            _currentHP = nv;
        }
    }
}
