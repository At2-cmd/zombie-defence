using UnityEngine;
using Zenject;

public class UIController : MonoBehaviour, IInitializable, IUIController
{
    [Inject] ILevelDataProvider _levelDataProvider;
    [SerializeField] private TimerView timerView;
    [SerializeField] private PopupBase successPopupView;
    [SerializeField] private PopupBase failedPopupView;
    public void Initialize()
    {
        timerView.Initialize(_levelDataProvider.LevelDuration);
    }
    public void ShowSuccessPopup()
    {
        successPopupView.SetPopupActiveness(true);
        successPopupView.Initialize();
    }
    public void ShowFailPopup()
    {
        failedPopupView.SetPopupActiveness(true);
        failedPopupView.Initialize();
    }

    public void StopTimer()
    {
        timerView.StopTimer();
    }
}
