using System.Collections.Generic;
using UnityEngine;

public class FireTrail : MonoBehaviour, IPoolable
{
    private Dictionary<MonsterController, float> stayTimeDic = new();

    [SerializeField] private float _lifeTime = 3f;

    private float _dotDamage;
    private float _t;

    void Update()
    {
        _t += Time.deltaTime;

        if (_t >= _lifeTime)
        {
            PoolManager.Instance.ReturnFireTrail(this);
        }
    }

    public void InitFireTrail(float dotDamage)
    {
        _dotDamage = dotDamage;
    }

    void OnTriggerStay(Collider other)
    {
        MonsterController monster = other.GetComponentInParent<MonsterController>();

        if (monster == null)
            return;

        if (!stayTimeDic.ContainsKey(monster))
            stayTimeDic.Add(monster, 0f);

        stayTimeDic[monster] += Time.deltaTime;

        if (stayTimeDic[monster] >= 0.5f)
        {
            monster.TakeDamage(_dotDamage);
            stayTimeDic[monster] = 0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        MonsterController monster = other.GetComponentInParent<MonsterController>();

        if (monster != null)
            stayTimeDic.Remove(monster);
    }

    public void OnSpawnFromPool()
    {
        gameObject.SetActive(true);
    }

    public void OnReturnToPool()
    {
        _t = 0f;
        _dotDamage = 0f;

        stayTimeDic.Clear();

        gameObject.SetActive(false);
    }
}