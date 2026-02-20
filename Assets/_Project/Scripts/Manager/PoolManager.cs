using UnityEngine;
using System.Collections.Generic;

public class PoolManager : SingleTon<PoolManager>
{
    #region Projectile
    [SerializeField] private GameObject _proj;
    private Queue<Projectile> _projPools = new Queue<Projectile>();
    private int _projPoolSize = 32;

    protected override void Awake()
    {
        base.Awake();
        InitPool();
    }

    void InitPool()
    {
        for(int i=0; i<_projPoolSize; i++)
        {
            Projectile projInstance = Instantiate(_proj, transform).GetComponent<Projectile>();
            projInstance.gameObject.SetActive(false);
            _projPools.Enqueue(projInstance);
        }
    }

    public Projectile GetProj()
    {
        Projectile proj = _projPools.Dequeue();
        proj.gameObject.SetActive(true);
        return proj;
    }

    public void ReturnProj(Projectile proj)
    {
        proj.gameObject.SetActive(false);
        _projPools.Enqueue(proj);
    }
    #endregion
}
