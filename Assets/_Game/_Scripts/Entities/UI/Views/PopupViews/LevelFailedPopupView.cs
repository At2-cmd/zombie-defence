using DG.Tweening;

public class LevelFailedPopupView : PopupBase
{
    public override void Initialize()
    {
        base.Initialize();
    }
    public void OnRetryLevelButtonClicked()
    {
        SetPopupActiveness(false);
        ShowBlackScreenOnTransition();
        EventController.Instance.RaiseLevelProceeded();
        //TODO: Retry current level.
    }
}
