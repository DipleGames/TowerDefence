using UnityEngine;

public class PoolManager : SingleTon<PoolManager>
{
    [Header("Projectile")]
    [SerializeField] private Projectile _projPrefab;
    [SerializeField] private Transform _projParent;
    [SerializeField] private int _projPoolSize = 32;

    [Header("Hit Effect")]
    [SerializeField] private HitEffect _hitEffectPrefab;
    [SerializeField] private Transform _hitEffectParent;
    [SerializeField] private int _hitEffectPoolSize = 32;

    [Header("Fire Trail")]
    [SerializeField] private FireTrail _fireTrailPrefab;
    [SerializeField] private Transform _fireTrailParent;
    [SerializeField] private int _fireTrailPoolSize = 32;

    private ObjectPool<Projectile> _projPool;
    private ObjectPool<HitEffect> _hitEffectPool;
    private ObjectPool<FireTrail> _fireTrailPool;

    protected override void Awake()
    {
        base.Awake();

        _projPool = new ObjectPool<Projectile>(_projPrefab, _projParent, _projPoolSize);
        _hitEffectPool = new ObjectPool<HitEffect>(_hitEffectPrefab, _hitEffectParent, _hitEffectPoolSize);
        _fireTrailPool = new ObjectPool<FireTrail>(_fireTrailPrefab, _fireTrailParent, _fireTrailPoolSize);
    }

    public Projectile GetProj()
    {
        return _projPool.Get();
    }

    public void ReturnProj(Projectile proj)
    {
        _projPool.Return(proj);
    }

    public HitEffect GetHitEffect()
    {
        return _hitEffectPool.Get();
    }

    public void ReturnHitEffect(HitEffect hitEffect)
    {
        _hitEffectPool.Return(hitEffect);
    }

    public FireTrail GetFireTrail()
    {
        return _fireTrailPool.Get();
    }

    public void ReturnFireTrail(FireTrail fireTrail)
    {
        _fireTrailPool.Return(fireTrail);
    }
}