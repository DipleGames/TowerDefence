using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public enum GameState { Wave, Prepare, Pause, GameOver }

public class GameManager : SingleTon<GameManager>
{
    [Header("현재 상태")]
    public GameState gameState = GameState.Wave;

    [Header("웨이브 길이")]
    [SerializeField] private float waveTime;

    void Start()
    {
        StartCoroutine(WaveCorutine());
    }


    void Update()
    {
        
    }

    public IEnumerator WaveCorutine()
    {
        while(gameState == GameState.Wave)
        {
            MonsterSpawner.Instance.SpawnMonster();
            yield return new WaitForSeconds(MonsterSpawner.Instance.spawnDelay);
        }
        gameState = GameState.Prepare;
        yield break;
    }
    
}
