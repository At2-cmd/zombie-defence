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
        _enemyController.StopWaveGeneration();

        var currentLevelIndex = SaverManager.Load(SaverManager.Keys.LastLevelIndex,0);
        SaverManager.Save(SaverManager.Keys.LastLevelIndex, currentLevelIndex + 1);
        _uiController.ShowSuccessPopup();
    }

    public void OnGameFailed()
    {
        _uiController.StopTimer();
        _enemyController.StopWaveGeneration();
        _uiController.ShowFailPopup();
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
