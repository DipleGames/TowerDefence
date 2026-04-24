using UnityEngine;
using System.Collections.Generic;

public class PoolManager : SingleTon<PoolManager>
{
    protected override void Awake()
    {
        base.Awake();
        InitProjPool();
        InithitEffectPool();
    }

    #region Projectile
    [SerializeField] private GameObject _proj;
    [SerializeField] private GameObject _projParent;
    private Queue<Projectile> _projPool = new Queue<Projectile>();
    private int _projPoolSize = 32;


    void InitProjPool()
    {
        for(int i=0; i<_projPoolSize; i++)
        {
            Projectile projInstance = Instantiate(_proj, _projParent.transform).GetComponent<Projectile>();
            projInstance.gameObject.SetActive(false);
            _projPool.Enqueue(projInstance);
        }
    }

    public Projectile GetProj()
    {
        Projectile proj = _projPool.Dequeue();
        proj.gameObject.SetActive(true);
        return proj;
    }

    public void ReturnProj(Projectile proj)
    {
        proj.gameObject.SetActive(false);
        _projPool.Enqueue(proj);
    }
    #endregion

    #region hitEffect
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private GameObject _hitEffectParent;
    private Queue<ParticleSystem> _hitEffectPool = new Queue<ParticleSystem>();
    private int _hitEffectPoolSize = 32;


    void InithitEffectPool()
    {
        for(int i=0; i<_hitEffectPoolSize; i++)
        {
            ParticleSystem hitEffectInstance = Instantiate(_hitEffect, _hitEffectParent.transform).GetComponent<ParticleSystem>();
            hitEffectInstance.gameObject.SetActive(false);
            _hitEffectPool.Enqueue(hitEffectInstance);
        }
    }

    public ParticleSystem GetHitEffect()
    {
        ParticleSystem hitEffect = _hitEffectPool.Dequeue();

        hitEffect.gameObject.SetActive(true);
        hitEffect.Clear();
        hitEffect.Play();

        return hitEffect;
    }

    public void ReturnHitEffect(ParticleSystem hitEffect)
    {
        hitEffect.gameObject.SetActive(false);
        _hitEffectPool.Enqueue(hitEffect);
    }
    #endregion
}
