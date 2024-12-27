using UnityEngine;

public abstract class PopupBase : MonoBehaviour
{
    [SerializeField] private FinishPopupBoard board;
    public virtual void Initialize()
    {
        board.PlayOpeningAnimation(null);
    }
    public void SetPopupActiveness(bool value)
    {
        gameObject.SetActive(value);
    }
}
