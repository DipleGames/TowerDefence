using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class IceTrail : MonoBehaviour, IPoolable
{
    [SerializeField] private float _lifeTime = 3f;
    private float _slowMultipler;
    private float _t;

    void Update()
    {
        _t += Time.deltaTime;

        if (_t >= _lifeTime)
        {
            PoolManager.Instance.Return<IceTrail>(this);
        }
    }

    public void InitIceTrail(float slowMultipler)
    {
        _slowMultipler = slowMultipler;
    }

    void OnTriggerEnter(Collider other)
    {
        MonsterController monster = other.GetComponentInParent<MonsterController>();
        if (monster == null)
            return;
        monster.ApplySlow(_slowMultipler);
    }

    void OnTriggerExit(Collider other)
    {
        MonsterController monster = other.GetComponentInParent<MonsterController>();
        if(monster != null)
        {
            monster.RemoveSlow();
        }
    }

    public void OnSpawnFromPool()
    {
        gameObject.SetActive(true);
    }

    public void OnReturnToPool()
    {
        _t = 0f;

        gameObject.SetActive(false);
    }
}
