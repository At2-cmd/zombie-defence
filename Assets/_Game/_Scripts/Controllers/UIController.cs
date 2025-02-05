using System;
using TMPro;
using UnityEngine;
using Zenject;

public class UIController : MonoBehaviour, IInitializable, IUIController
{
    [Inject] ILevelDataProvider _levelDataProvider;
    [Inject] IEnemyController _enemyController;
    [SerializeField] private TimerView timerView;
    [SerializeField] private PlayerHealthBarView playerHealthBarView;
    [SerializeField] private PopupBase successPopupView;
    [SerializeField] private PopupBase failedPopupView;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text killedEnemyCounterText;
    public void Initialize()
    {
        Subscribe();
        SetLevelText();
        SetKilledEnemyCountText();
        SetPlayerHealthBar(1);
        timerView.Initialize(_levelDataProvider.LevelDuration);
    }
    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        EventController.Instance.OnEnemyKilled += OnEnemyKilledHandler;
        EventController.Instance.OnLevelProceeded += OnLevelProceededHandler;
    }

    private void Unsubscribe()
    {
        EventController.Instance.OnEnemyKilled -= OnEnemyKilledHandler;
        EventController.Instance.OnLevelProceeded -= OnLevelProceededHandler;
    }

    private void OnLevelProceededHandler()
    {
        SetLevelText();
        SetKilledEnemyCountText();
        SetPlayerHealthBar(1);
        timerView.Initialize(_levelDataProvider.LevelDuration);
    }
    private void OnEnemyKilledHandler()
    {
        SetKilledEnemyCountText();
    }

    private void SetKilledEnemyCountText()
    {
        killedEnemyCounterText.text = _enemyController.KilledEnemyCountInLevel.ToString();
    }

    public void ShowSuccessPopup()
    {
        successPopupView.SetPopupActiveness(true);
        successPopupView.Initialize();
        successPopupView.SetKillStats(_enemyController.KilledEnemyCountInLevel, SaverManager.Load(SaverManager.Keys.TotalKilledEnemy,0));
    }
    public void ShowFailPopup()
    {
        failedPopupView.SetPopupActiveness(true);
        failedPopupView.Initialize();
        failedPopupView.SetKillStats(_enemyController.KilledEnemyCountInLevel, SaverManager.Load(SaverManager.Keys.TotalKilledEnemy,0));
    }

    public void StopTimer()
    {
        timerView.StopTimer();
    }

    private void SetLevelText()
    {
        levelText.text = "Level " + (SaverManager.Load(SaverManager.Keys.LastLevelIndex, 0) + 1);
    }

    public void SetPlayerHealthBar(float percentageAmount)
    {
        playerHealthBarView.SetPlayerHealthBarView(percentageAmount);
    }
}
