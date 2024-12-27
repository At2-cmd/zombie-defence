using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour, IInitializable, IGameManager
{
    [Inject] IUIController _uiController;
    public void Initialize()
    {

    }
    public void OnGameSuccessed()
    {
        _uiController.ShowSuccessPopup();
    }
    public void OnGameFailed()
    {
        _uiController.ShowFailPopup();
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            OnGameSuccessed();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnGameFailed();
        }
    }
#endif
}
