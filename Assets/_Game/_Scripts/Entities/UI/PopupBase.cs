using _Game.Scripts.Entities.UI;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public abstract class PopupBase : MonoBehaviour
{
    [Inject] BlackScreen _blackScreen;
    [SerializeField] private FinishPopupBoard board;
    [SerializeField] private TMP_Text killedInLevelText;
    [SerializeField] private TMP_Text totalKilledText;

    public virtual void Initialize()
    {
        board.PlayOpeningAnimation(null);
    }
    public void SetPopupActiveness(bool value)
    {
        gameObject.SetActive(value);
    }
    public void SetKillStats(int killedInLevelCount, int totalKilledCount)
    {
        killedInLevelText.text = killedInLevelCount.ToString();
        totalKilledText.text = totalKilledCount.ToString();
    }

    public void ShowBlackScreenOnTransition()
    {
        _blackScreen.Open(() =>
        {
            DOVirtual.DelayedCall(.5f, _blackScreen.Close);
        });
    }
}
