using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIController : MonoBehaviour, IInitializable
{
    [Inject] ILevelDataProvider _levelDataProvider;
    [SerializeField] private TimerView timerView;
    public void Initialize()
    {
        timerView.Initialize(_levelDataProvider.LevelDuration);
    }
}
