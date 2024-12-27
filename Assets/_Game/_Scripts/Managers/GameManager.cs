using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour, IInitializable, IGameManager
{
    [Inject] IUIController _uiController;
    [Inject] IEnemyController _enemyController;
    public void Initialize()
    {

    }
    public void OnGameSuccessed()
    {
        _uiController.StopTimer();
        _uiController.ShowSuccessPopup();
        _enemyController.StopWaveGeneration();
        _enemyController.DespawnAllActiveEnemies();

        var currentLevelIndex = SaverManager.Load(SaverManager.Keys.LastLevelIndex,0);
        SaverManager.Save(SaverManager.Keys.LastLevelIndex, currentLevelIndex + 1);
    }

    public void OnGameFailed()
    {
        _uiController.StopTimer();
        _uiController.ShowFailPopup();
        _enemyController.StopWaveGeneration();
        _enemyController.DespawnAllActiveEnemies();
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
