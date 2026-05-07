using UnityEngine;
using System.Collections;
using System;

public enum GameState { Wave, Prepare, Pause, GameOver }

public class GameManager : SingleTon<GameManager>
{
    public float gameTime = 0f;

    [Header("현재 상태")]
    public GameState gameState = GameState.Wave;

    [Header("현재 웨이브")]
    public int currWave = 0;

    [Header("게임 상태별 길이")]
    public float waveTime;
    public float prepareTime;

    private Coroutine gameLoopRoutine;
    private Coroutine stateRoutine;
    private Coroutine timerRoutine;
    public event Action<GameState> OnChangedGameState;

    protected override void Awake()
    {
        base.Awake();
        OnChangedGameState += HUDManager.Instance.OnChangeGameState;
    }

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
    private GameState _lastState;
    public void SetPause(bool isPause)
    {
        if (gameState == GameState.GameOver) return;

        if (isPause)
        {
            if (gameState == GameState.Pause) return;

            _lastState = gameState;
            gameState = GameState.Pause;
            Time.timeScale = 0f;
        }
        else
        {
            if (gameState != GameState.Pause) return;

            Time.timeScale = 1f;
            gameState = _lastState;
        }
    }

    // -----------------------------
    // Core Loop
    // -----------------------------
    public bool isReady = false;
    private IEnumerator GameLoopCoroutine()
    {
        while (true)
        {
            // GameOver면 루프 종료
            if (gameState == GameState.GameOver)
                yield break;

            // 1) Wave
            gameState = GameState.Wave;
            currWave += 1;
            isReady = false;
            OnChangedGameState.Invoke(gameState);
            StartStateRoutine(WaveCoroutine());
            StartTimerRoutine(HUDManager.Instance.StartTimer(waveTime)); 
            yield return new WaitForSeconds(waveTime);
            StopStateRoutine();
            StopTimerRoutine();

            if (gameState == GameState.GameOver)
                yield break;

            // 2) Prepare
            gameState = GameState.Prepare;
            OnChangedGameState.Invoke(gameState);
            StartStateRoutine(PrepareCoroutine());
            StartTimerRoutine(HUDManager.Instance.StartTimer(prepareTime));
            yield return new WaitUntil(() => isReady == true);
            StopStateRoutine();
            StopTimerRoutine();
        }
    }

    public void Ready()
    {
        isReady = true;
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

    private void StartTimerRoutine(IEnumerator routine)
    {
        StopTimerRoutine(); // 중복 실행 방지
        timerRoutine = StartCoroutine(routine);
    }

    private void StopTimerRoutine()
    {
        if (timerRoutine != null)
        {
            StopCoroutine(timerRoutine);
            timerRoutine = null;
        }
    }

    // -----------------------------
    // State Coroutines
    // -----------------------------
    private IEnumerator WaveCoroutine()
    {
        int spawnIndex = 0;
        while (gameState == GameState.Wave)
        {
            if(currWave%5 != 0)
            {
                MonsterSpawner.Instance.SpawnMonster();
                spawnIndex++;
            }
            else if(currWave%5 == 0 && spawnIndex == 0)
            {
                MonsterSpawner.Instance.SpawnBoss();
                spawnIndex++;
            }
            else if(currWave%5 == 0 && spawnIndex != 0)
            {
                MonsterSpawner.Instance.SpawnMonster();
                spawnIndex++;
            }
            yield return new WaitForSeconds(MonsterSpawner.Instance.spawnDelay);
        }
    }

    private IEnumerator PrepareCoroutine()
    {
        StartCoroutine(AugmentManager.Instance.AugmentPhase(prepareTime));

        float _tick = 0f;
        while (gameState == GameState.Prepare)
        {
            _tick += Time.deltaTime;
            if(_tick > prepareTime)
            {
                isReady = true;
            }
            yield return null;
        }
    }
}
