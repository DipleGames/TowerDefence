using UnityEngine;

public class HitEffect : MonoBehaviour, IPoolable
{
    private ParticleSystem _ps;

    private void Awake()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!_ps.IsAlive(true))
        {
            PoolManager.Instance.Return<HitEffect>(this);
        }
    }

    public void OnSpawnFromPool()
    {
        gameObject.SetActive(true);

        _ps.Clear();
        _ps.Play();
    }

    public void OnReturnToPool()
    {
        _ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        gameObject.SetActive(false);
    }
}