using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private MonsterFactory factory;

    [SerializeField] private float _spawnDelay = 3f;
    private float _tick = 0f;
    void Update()
    {
        _tick += Time.deltaTime;
        if(_tick >= _spawnDelay)
        {
            SpawnMonster();
            _tick = 0f;
        }
    }

    void SpawnMonster()
    {
        factory.Create(MonsterType.Monster, transform.position);
    }
}
