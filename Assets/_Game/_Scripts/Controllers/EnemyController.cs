using System.Collections;
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
    }

    private void Unsubscribe()
    {
        EventController.Instance.OnLevelProceeded -= OnLevelProceededHandler;
    }

    private void OnLevelProceededHandler()
    {
        _durationBetweenWaves = new WaitForSeconds(_levelDataProvider.WaveGenerationDelayForLevel);
        GenerateWave();
        StartWaveGeneration();
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
}
