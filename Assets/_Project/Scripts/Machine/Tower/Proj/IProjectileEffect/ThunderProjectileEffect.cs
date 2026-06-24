using UnityEngine;

public class ThunderProjectileEffect : IProjectileEffect
{
    private float _thunderDamage;
    private float _stunTime;

    public ThunderProjectileEffect(float thunderDamage, float stunTime)
    {
        _thunderDamage = thunderDamage;
        _stunTime = stunTime;
    }

    public void Apply(MonsterController target)
    {
        Spark spark = PoolManager.Instance.GetSpark();
        spark.InitSpark(_thunderDamage);
        target.ApplyStun(_stunTime);
        spark.transform.position = target.transform.position;
        spark.transform.rotation = Quaternion.identity;
    }
}