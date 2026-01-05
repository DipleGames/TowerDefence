using UnityEngine;
using System.Collections;

public enum GameState { Wave, Prepare, Pause, GameOver }

public class GameManager : SingleTon<GameManager>
{
    public float gameTime = 0f;

    [Header("현재 상태")]
    public GameState gameState = GameState.Wave;

    [Header("게임 상태별 길이")]
    [SerializeField] private float waveTime;
    [SerializeField] private float prepareTime;

    private Coroutine gameLoopRoutine;
    private Coroutine stateRoutine;

    private void Start()
    {
        StartGameLoop();
    }

    private void OnDisable()
    {
        StopGameLoop();
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
    }

    // -----------------------------
    // Public Controls
    // -----------------------------
    public void StartGameLoop()
    {
        if (gameLoopRoutine != null) return;
        gameLoopRoutine = StartCoroutine(GameLoopCoroutine());
    }

    public void StopGameLoop()
    {
        if (gameLoopRoutine != null)
        {
            StopCoroutine(gameLoopRoutine);
            gameLoopRoutine = null;
        }

        StopStateRoutine();
    }

    public void SetGameOver()
    {
        gameState = GameState.GameOver;
        StopGameLoop();
    }

    /// <summary>
    /// 옵션창 등 Pause 진입 (Time.timeScale = 0)
    /// </summary>
    public void SetPause(bool isPause)
    {
        if (gameState == GameState.GameOver) return;

        if (isPause)
        {
            gameState = GameState.Pause;
            Time.timeScale = 0f;  // WaitForSeconds도 멈춤
        }
        else
        {
            Time.timeScale = 1f;

            // Pause 해제 시 어느 상태로 돌아갈지 정책이 필요함.
            // 여기서는 "Prepare로 복귀"로 해둠. 원하면 lastState 저장해서 복귀도 가능.
            gameState = GameState.Prepare;
        }
    }

    // -----------------------------
    // Core Loop
    // -----------------------------
    private IEnumerator GameLoopCoroutine()
    {
        while (true)
        {
            // GameOver면 루프 종료
            if (gameState == GameState.GameOver)
                yield break;

            // 1) Wave
            gameState = GameState.Wave;
            StartStateRoutine(WaveCoroutine());
            yield return new WaitForSeconds(waveTime);
            StopStateRoutine();

            if (gameState == GameState.GameOver)
                yield break;

            // 2) Prepare
            gameState = GameState.Prepare;
            StartStateRoutine(PrepareCoroutine());
            yield return new WaitForSeconds(prepareTime);
            StopStateRoutine();
        }
    }

    private void StartStateRoutine(IEnumerator routine)
    {
        StopStateRoutine(); // 중복 실행 방지
        stateRoutine = StartCoroutine(routine);
    }

    private void StopStateRoutine()
    {
        if (stateRoutine != null)
        {
            StopCoroutine(stateRoutine);
            stateRoutine = null;
        }
    }

    // -----------------------------
    // State Coroutines
    // -----------------------------
    private IEnumerator WaveCoroutine()
    {
        while (gameState == GameState.Wave)
        {
            if (MonsterSpawner.Instance != null)
            {
                MonsterSpawner.Instance.SpawnMonster();
                yield return new WaitForSeconds(MonsterSpawner.Instance.spawnDelay);
            }
        }
    }

    private IEnumerator PrepareCoroutine()
    {
        while (gameState == GameState.Prepare)
        {
            yield return null;
        }
    }
}
