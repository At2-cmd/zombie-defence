using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyController : MonoBehaviour, IInitializable, IEnemyController
{
    [Inject] private EnemyEntity.Pool _enemyPool;
    [Inject] private ILevelDataProvider _levelDataProvider;
    [Inject] private IPlayerControllerDataProvider _playerControllerDataProvider;
    [SerializeField] private int _initialEnemySpawnAmount;

    private bool _isWaveGenerationActive;
    private Coroutine _waveGenerationRoutine;
    private WaitForSeconds _durationBetweenWaves;
    private List<EnemyEntity> _activeEnemiesList = new();

    public int KilledEnemyCountInLevel { get; set; }

    public void Initialize()
    {
        Subscribe();
        _durationBetweenWaves = new WaitForSeconds(_levelDataProvider.WaveGenerationDelayForLevel);
        GenerateWave();
        StartWaveGeneration();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
    private void Subscribe()
    {
        EventController.Instance.OnLevelProceeded += OnLevelProceededHandler;
        EventController.Instance.OnEnemyKilled += OnEnemyKilledHandler;
    }

    private void Unsubscribe()
    {
        EventController.Instance.OnLevelProceeded -= OnLevelProceededHandler;
        EventController.Instance.OnEnemyKilled -= OnEnemyKilledHandler;
    }

    private void OnLevelProceededHandler()
    {
        KilledEnemyCountInLevel = 0;
        _activeEnemiesList.Clear();
        _durationBetweenWaves = new WaitForSeconds(_levelDataProvider.WaveGenerationDelayForLevel);
        GenerateWave();
        StartWaveGeneration();
    }

    private void OnEnemyKilledHandler()
    {
        KilledEnemyCountInLevel++;
    }

    private IEnumerator WaveGenerationCoroutine()
    {
        while (_isWaveGenerationActive)
        {
            yield return _durationBetweenWaves;
            GenerateWave();
        }
    }

    private void GenerateWave()
    {
        if (_playerControllerDataProvider.PlayerTransform == null) return;

        var playerPos = _playerControllerDataProvider.PlayerTransform.position;
        int waveAmount = _levelDataProvider.WaveGenerationAmountForLevel;

        for (int i = 0; i < waveAmount; i++)
        {
            var randomSpawnPosAroundPlayer = new Vector3(Random.Range(playerPos.x - 10, playerPos.x + 10),0,Random.Range(playerPos.z - 10, playerPos.z + 10));
            var enemy = _enemyPool.Spawn(randomSpawnPosAroundPlayer);
            enemy.SetEnemyInitialHealth(1);
            _activeEnemiesList.Add(enemy);
        }
    }

    public void StopWaveGeneration()
    {
        _isWaveGenerationActive = false;
        if (_waveGenerationRoutine != null)
        {
            StopCoroutine(_waveGenerationRoutine);
        }
    }

    public void StartWaveGeneration()
    {
        if (!_isWaveGenerationActive)
        {
            _isWaveGenerationActive = true;
            _waveGenerationRoutine = StartCoroutine(WaveGenerationCoroutine());
        }
    }

    public void DespawnAllActiveEnemies()
    {
        foreach (var enemy in _activeEnemiesList)
        {
            enemy.Despawn();
        }
    }
}
