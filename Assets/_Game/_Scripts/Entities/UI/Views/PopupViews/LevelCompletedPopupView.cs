using DG.Tweening;

public class LevelCompletedPopupView : PopupBase
{
    public override void Initialize()
    {
        base.Initialize();
    }

    public void OnNextLevelButtonClicked()
    {
        SetPopupActiveness(false);
        ShowBlackScreenOnTransition();
        //TODO: Load next level.
    }
}