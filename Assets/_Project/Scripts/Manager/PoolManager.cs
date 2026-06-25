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

    [Header("Ice Trail")]
    [SerializeField] private IceTrail _iceTrailPrefab;
    [SerializeField] private Transform _iceTrailParent;
    [SerializeField] private int _iceTrailPoolSize = 32;

    [Header("Spark")]
    [SerializeField] private Spark _sparkPrefab;
    [SerializeField] private Transform _sparkParent;
    [SerializeField] private int _sparkPoolSize = 32;


    private ObjectPool<Projectile> _projPool;
    private ObjectPool<HitEffect> _hitEffectPool;
    private ObjectPool<FireTrail> _fireTrailPool;
    private ObjectPool<IceTrail> _iceTrailPool;
    private ObjectPool<Spark> _sparkPool;

    protected override void Awake()
    {
        base.Awake();

        _projPool = new ObjectPool<Projectile>(_projPrefab, _projParent, _projPoolSize);
        _hitEffectPool = new ObjectPool<HitEffect>(_hitEffectPrefab, _hitEffectParent, _hitEffectPoolSize);
        _fireTrailPool = new ObjectPool<FireTrail>(_fireTrailPrefab, _fireTrailParent, _fireTrailPoolSize);
        _iceTrailPool = new ObjectPool<IceTrail>(_iceTrailPrefab, _iceTrailParent, _iceTrailPoolSize);
        _sparkPool = new ObjectPool<Spark>(_sparkPrefab, _sparkParent, _sparkPoolSize);
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

    public IceTrail GetIceTrail()
    {
        return _iceTrailPool.Get();
    }

    public void ReturnIceTrail(IceTrail iceTrail)
    {
        _iceTrailPool.Return(iceTrail);
    }

    public Spark GetSpark()
    {
        return _sparkPool.Get();
    }

    public void ReturnSpark(Spark spark)
    {
        _sparkPool.Return(spark);
    }
}