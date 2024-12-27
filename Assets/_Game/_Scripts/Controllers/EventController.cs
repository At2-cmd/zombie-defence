using System;
using UnityEngine;
using Zenject;

public class EventController : MonoBehaviour, IInitializable
{
    public static EventController Instance { get; private set; }
    public void Initialize()
    {
        Instance = this;
    }

    public event Action OnLevelProceeded;
    public void RaiseLevelProceeded() => OnLevelProceeded?.Invoke();
}
