using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Entities.UI
{
    public class BlackScreen : MonoBehaviour
    {
        [SerializeField] private float openTime;
        [SerializeField] private float closeTime;
        [SerializeField] private Ease openEase;
        [SerializeField] private Ease closeEase;
        [SerializeField] private Image blackImage;
        
        public void Open(Action onOpen)
        {
            ChangeAlpha(blackImage, 0f);
            blackImage.gameObject.SetActive(true);
            blackImage.DOFade(1f, openTime).SetEase(openEase).OnComplete(() => onOpen?.Invoke());
        }

        public void Close()
        {
            blackImage.DOFade(0f, closeTime).SetEase(closeEase).OnComplete(() =>
            {
                blackImage.gameObject.SetActive(false);
            });
        }
        private void ChangeAlpha(Graphic graphic, float alpha)
        {
            var color = graphic.color;
            color.a = alpha;
            graphic.color = color;
        }
    }
}