using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour, IInitializable, IGameManager
{
    [Inject] IUIController _uiController;
    [Inject] IEnemyController _enemyController;
    [Inject] IPlayerController _playerController;
    public void Initialize()
    {

    }
    public void OnGameSuccessed()
    {
        _uiController.ShowSuccessPopup();
        _uiController.StopTimer();
        _enemyController.StopWaveGeneration();
        _enemyController.DespawnAllActiveEnemies();
        _playerController.SetPlayableStatusOfPlayer(false);

        int currentLevelIndex = SaverManager.Load(SaverManager.Keys.LastLevelIndex,0);
        int totalKilledEnemyCount = SaverManager.Load(SaverManager.Keys.TotalKilledEnemy,0);

        SaverManager.Save(SaverManager.Keys.LastLevelIndex, currentLevelIndex + 1);
        SaverManager.Save(SaverManager.Keys.TotalKilledEnemy, totalKilledEnemyCount + _enemyController.KilledEnemyCountInLevel);
    }

    public void OnGameFailed()
    {
        _uiController.ShowFailPopup();
        _uiController.StopTimer();
        _enemyController.StopWaveGeneration();
        _enemyController.DespawnAllActiveEnemies();
        _playerController.SetPlayableStatusOfPlayer(false);
    }


#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            OnGameSuccessed();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnGameFailed();
        }
    }
#endif
}
