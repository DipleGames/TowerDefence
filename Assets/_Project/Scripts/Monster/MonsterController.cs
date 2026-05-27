using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour, IDamageable
{
    [SerializeField] private Slider _slider;
    public MonsterModel monsterModel;
    private Quaternion _initialRotation;

    void Awake()
    {
        InitMonster();
    }

    private void Start()
    {
        _initialRotation = _slider.transform.rotation;
    }

    private void LateUpdate()
    {
        _slider.transform.rotation = _initialRotation;
    }

    public void InitMonster()
    {
        monsterModel.CurrentHP = monsterModel.maxHP;
        ViewManager.Instance.monsterView.UpdateHPBar(monsterModel, _slider);
    }



    public void TakeDamage(float amount)
    {
        monsterModel.CurrentHP -= amount;
        ParticleSystem hitEffect = PoolManager.Instance.GetHitEffect();
        hitEffect.transform.position = transform.position;
        ViewManager.Instance.monsterView.UpdateHPBar(monsterModel, _slider);
    }

    public void Stun()
    {
        
    }
}
