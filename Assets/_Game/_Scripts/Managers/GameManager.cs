using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour, IInitializable, IGameManager
{
    public void Initialize()
    {
        Debug.Log("Game Manager Initialized");
    }
    public void OnGameSuccessed()
    {
        Debug.Log("Game Successed");
    }
    public void OnGameFailed()
    {
        Debug.Log("Game Failed");
    }
}
