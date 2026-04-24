using UnityEngine;

public class HitEffect : MonoBehaviour
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
            PoolManager.Instance.ReturnHitEffect(_ps);
        }
    }
}
