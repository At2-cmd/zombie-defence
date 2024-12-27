using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelLoaderController : MonoBehaviour, IInitializable, ILevelDataProvider
{
    [Inject] DiContainer _container;
    [SerializeField] private List<LevelScriptableObject> levelDatas;
    [SerializeField] private int levelIndex;

    private GameObject _currentLevelPrefab;
    public int WaveGenerationAmountForLevel => levelDatas[levelIndex].WaveGenerationAmount;
    public float WaveGenerationDelayForLevel => levelDatas[levelIndex].WaveGenerationDelay;
    public float LevelDuration => levelDatas[levelIndex].LevelDuration;

    public void Initialize()
    {
        Subscribe();
        GenerateLevelPrefab();
    }
    private void Subscribe()
    {

    }

    private void Unsubscribe()
    {

    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void OnLevelGeneratedHandler()
    {
        GenerateLevelPrefab();
    }
    private void GenerateLevelPrefab()
    {
        if (_currentLevelPrefab) Destroy(_currentLevelPrefab);
        _currentLevelPrefab = _container.InstantiatePrefab(levelDatas[levelIndex].LevelPrefab);
        EventController.Instance.RaiseLevelGenerated();
    }

    private int CalculateLoopedIndex(int levelIndex)
    {
        return levelIndex % (levelDatas.Count);
    }
}
