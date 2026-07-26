using UnityEngine;

public class IceBlastProjectileEffect : IProjectileEffect
{
    private float _slowMultipler;

    public IceBlastProjectileEffect(float slowMultipler)
    {
        _slowMultipler = slowMultipler;
    }

    public void Apply(MonsterController target)
    {
        Debug.Log("아이스 블라스트 적용");

        IceTrail iceTrail = PoolManager.Instance.Get<IceTrail>();
        iceTrail.InitIceTrail(_slowMultipler); // 얼음 잔상 초기화

        iceTrail.transform.position = target.transform.position;
        iceTrail.transform.rotation = Quaternion.identity;
    }
}
