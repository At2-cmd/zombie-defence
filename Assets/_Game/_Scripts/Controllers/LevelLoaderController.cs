using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelLoaderController : MonoBehaviour, IInitializable, ILevelDataProvider
{
    [Inject] DiContainer _container;
    [SerializeField] private List<LevelScriptableObject> levelDatas;
    private int _loopedLevelIndex => CalculateLoopedIndex(SaverManager.Load(SaverManager.Keys.LastLevelIndex,0));

    private GameObject _currentLevelPrefab;
    public int WaveGenerationAmountForLevel => levelDatas[_loopedLevelIndex].WaveGenerationAmount;
    public float WaveGenerationDelayForLevel => levelDatas[_loopedLevelIndex].WaveGenerationDelay;
    public float PerEnemyHealthForLevel => levelDatas[_loopedLevelIndex].PerEnemyHealth;
    public float LevelDuration => levelDatas[_loopedLevelIndex].LevelDuration;

    public void Initialize()
    {
        Subscribe();
        GenerateLevelPrefab();
    }
    private void Subscribe()
    {
        EventController.Instance.OnLevelProceeded += OnLevelProceededHandler;
    }

    private void Unsubscribe()
    {
        EventController.Instance.OnLevelProceeded -= OnLevelProceededHandler;
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void OnLevelProceededHandler()
    {
        GenerateLevelPrefab();
    }
    private void GenerateLevelPrefab()
    {
        if (_currentLevelPrefab) Destroy(_currentLevelPrefab);
        _currentLevelPrefab = _container.InstantiatePrefab(levelDatas[_loopedLevelIndex].LevelPrefab);
    }

    private int CalculateLoopedIndex(int levelIndex)
    {
        return levelIndex % (levelDatas.Count);
    }
}
