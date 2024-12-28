using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarView : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;
    private float _fillTime = .1f;
    private Tweener _healthBarTween;

    public void SetPlayerHealthBarView(float percentageAmount)
    {
        _healthBarTween?.Kill();
        _healthBarTween = healthBarSlider.DOValue(percentageAmount, _fillTime);
    }
}
