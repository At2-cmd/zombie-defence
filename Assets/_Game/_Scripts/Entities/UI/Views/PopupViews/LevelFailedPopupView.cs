public class LevelFailedPopupView : PopupBase
{
    public override void Initialize()
    {
        base.Initialize();
    }
    public void OnRetryLevelButtonClicked()
    {
        SetPopupActiveness(false);
        //TODO: Retry current level.
    }
}
