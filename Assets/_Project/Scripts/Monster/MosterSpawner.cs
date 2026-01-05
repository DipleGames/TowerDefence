using UnityEngine;

public class MonsterSpawner : SingleTon<MonsterSpawner>
{
    [SerializeField] private MonsterFactory factory;

    public float spawnDelay = 3f;

    public void SpawnMonster()
    {
        factory.Create(MonsterType.Monster, transform.position);
    }
}
