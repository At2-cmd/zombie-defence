using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public class TimerView : MonoBehaviour
{
    [Inject] IGameManager _gameManager;
    [SerializeField] private TMP_Text durationText;
    private float _levelDuration;
    private Coroutine _timerCoroutine;
    private WaitForSeconds _oneSecond = new WaitForSeconds(1);
    public void Initialize(float levelDuration)
    {
        _levelDuration = levelDuration;
        _timerCoroutine = StartCoroutine(StartCountdown());
    }

    private void SetDurationText(float remainingTime)
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        durationText.text = $"{minutes:00}:{seconds:00}";
    }

    private IEnumerator StartCountdown()
    {
        float remainingTime = _levelDuration;
        while (remainingTime > 0)
        {
            SetDurationText(remainingTime);
            yield return _oneSecond;
            remainingTime--;
        }

        SetDurationText(0);
        _gameManager.OnGameSuccessed();
    }

    public void StopTimer()
    {
        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
        }
    }
}
