using UnityEngine;
using Zenject;

public class InputController : MonoBehaviour, IInitializable, IInputDataProvider
{
    [SerializeField] private FloatingJoystick _floatingJoystick;

    public float VerticalInput => _floatingJoystick.Vertical;
    public float HorizontalInput => _floatingJoystick.Horizontal;

    public void Initialize()
    {
        if (_floatingJoystick == null)
        {
            Debug.LogWarning("No Joystick is assigned on InputController!");
        }
    }
}
