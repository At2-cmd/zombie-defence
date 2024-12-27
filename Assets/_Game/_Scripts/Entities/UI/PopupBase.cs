using _Game.Scripts.Entities.UI;
using DG.Tweening;
using UnityEngine;
using Zenject;

public abstract class PopupBase : MonoBehaviour
{
    [Inject] BlackScreen _blackScreen;
    [SerializeField] private FinishPopupBoard board;
    public virtual void Initialize()
    {
        board.PlayOpeningAnimation(null);
    }
    public void SetPopupActiveness(bool value)
    {
        gameObject.SetActive(value);
    }

    public void ShowBlackScreenOnTransition()
    {
        _blackScreen.Open(() =>
        {
            DOVirtual.DelayedCall(.5f, _blackScreen.Close);
        });
    }
}
