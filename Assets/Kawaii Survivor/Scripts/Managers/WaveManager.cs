using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEditor.Experimental.GraphView;

[RequireComponent(typeof(WaveManagerUI))]   
public class WaveManager : MonoBehaviour, IGameStateListener
{
    [Header("Elements")]
    [SerializeField] private Player player;
    private WaveManagerUI waveMangerUI;

    [Header("Settings")]
    [SerializeField] private float waveDuration;
    private float timer;
    private bool isTimerOn;
    private int currentWaveIndex;

    [Header("Waves")]
    [SerializeField] private Wave[] waves;
    private List<float> localCounters = new List<float>();

    private void Awake()
    {
        waveMangerUI = GetComponent<WaveManagerUI>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isTimerOn) return;

        if (timer < waveDuration)
        {
            ManageCurrentWave();

            waveMangerUI.UpdateWaveText("Wave " + (currentWaveIndex + 1) + " / " + waves.Length);

            string timerString = ((int)(waveDuration - timer)).ToString();
            waveMangerUI.UpdateTimerText(timerString);
        }
        else
            StartWaveTransition();
    }

    private void StartWave(int waveIndex)
    {
        localCounters.Clear();
        foreach(WaveSegment segment in waves[waveIndex].segments)
        {
            localCounters.Add(1);
        }

        timer = 0;

        isTimerOn = true;
    }

    private void ManageCurrentWave()
    {
        Wave currentWave = waves[currentWaveIndex];

        for (int i = 0; i < currentWave.segments.Count; i++)
        {
            WaveSegment segment = currentWave.segments[i];

            float tStart = segment.tStartEnd.x / 100 * waveDuration;
            float tEnd   = segment.tStartEnd.y / 100 * waveDuration;

            if (timer < tStart || timer > tEnd) continue;

            float timeSinceSegmentStart = timer - tStart;

            float spawnDelay = 1f / segment.spawnFrequency;
            if (timeSinceSegmentStart / spawnDelay > localCounters[i])
            {
                Instantiate(segment.prefab, GetSpawnPosition(), Quaternion.identity, transform);
                localCounters[i]++;

                if (segment.spawnOnce)
                    localCounters[i] += Mathf.Infinity;
            }
        }

        timer += Time.deltaTime;
    }

    private void StartWaveTransition()
    {
        isTimerOn = false;

        DefeatAllEnemies(); 

        currentWaveIndex++;

        if (currentWaveIndex >= waves.Length)
        {
            waveMangerUI.UpdateTimerText("");
            waveMangerUI.UpdateWaveText("Stage Completed");

            GameManager.instance.SetGameState(GameState.STAGECOMPLETE); 
        }
        else
        {
            GameManager.instance.WaveCompletedCallback();
        }
    }

    private void StartNextWave()
    {
        StartWave(currentWaveIndex);
    }

    private void DefeatAllEnemies()
    {
        foreach (Enemy enemy in transform.GetComponentsInChildren<Enemy>())
        {
            enemy.PassAwayAfterWave();
        }
    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 direction = Random.onUnitSphere;
        Vector2 offset = direction.normalized * Random.Range(6, 10);
        Vector2 targetPosition = (Vector2)player.transform.position + offset;

        if (!GameManager.instance.UseInfiniteMap)
        {
            targetPosition.x = Mathf.Clamp(targetPosition.x, -Constants.arenaSize.x / 2, Constants.arenaSize.x / 2);
            targetPosition.y = Mathf.Clamp(targetPosition.y, -Constants.arenaSize.x / 4, Constants.arenaSize.x / 4);
        }

        return targetPosition;
    }

    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GAME:
                StartNextWave();
                break;

            case GameState.GAMEOVER:
                isTimerOn = false;
                currentWaveIndex = 0;
                DefeatAllEnemies();
                break;
        }
    }
}

[System.Serializable]
public struct Wave
{
    public string name;
    public List<WaveSegment> segments;
}

[System.Serializable]
public struct WaveSegment
{
    [MinMaxSlider(0, 100)] public Vector2 tStartEnd;
    public float spawnFrequency;
    public GameObject prefab;
    public bool spawnOnce;
}