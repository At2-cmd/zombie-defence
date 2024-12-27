public class LevelCompletedPopupView : PopupBase
{
    public override void Initialize()
    {
        base.Initialize();
    }

    public void OnNextLevelButtonClicked()
    {
        SetPopupActiveness(false);
        //TODO: Load next level.
    }
}