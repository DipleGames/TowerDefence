using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour, IDamageable
{
    [Header("참조")]
    public MonsterModel monsterModel;
    public MonsterStateMachine monsterStateMachine;
    [SerializeField] private Slider _slider;
    private Quaternion _initialRotation;
    private float originSpeed;

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
        originSpeed = monsterStateMachine.agent.speed;
    }


    public void TakeDamage(float amount)
    {
        monsterModel.CurrentHP -= amount;
        HitEffect hitEffect = PoolManager.Instance.GetHitEffect();
        hitEffect.transform.position = transform.position;
        ViewManager.Instance.monsterView.UpdateHPBar(monsterModel, _slider);
    }


    #region  Stun
    private Coroutine stunCoroutine;
    public void ApplyStun(float duration)
    {
        var agent = monsterStateMachine.agent;

        if (agent == null || !agent.enabled || !agent.isOnNavMesh)
            return;

        if (stunCoroutine != null)
            StopCoroutine(stunCoroutine);

        stunCoroutine = StartCoroutine(StunRoutine(duration));
    }

    private IEnumerator StunRoutine(float duration)
    {
        var agent = monsterStateMachine.agent;

        if (agent == null || !agent.enabled || !agent.isOnNavMesh)
            yield break;

        agent.isStopped = true;

        yield return new WaitForSeconds(duration);

        if (agent != null && agent.enabled && agent.isOnNavMesh)
            agent.isStopped = false;

        stunCoroutine = null;
    }
    #endregion

    #region  Slow
    public void ApplySlow(float multiplier)
    {
        monsterStateMachine.agent.speed = originSpeed * multiplier;
    }

    public void RemoveSlow()
    {
        monsterStateMachine.agent.speed = originSpeed;
    }
    #endregion
}
