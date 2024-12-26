using UnityEngine;
using Zenject;

public class InputController : MonoBehaviour, IInitializable, IInputDataProvider
{
    [SerializeField] private FloatingJoystick _floatingJoystick;
    private Vector3 _movementVector;
    public float VerticalInput => _floatingJoystick.Vertical;
    public float HorizontalInput => _floatingJoystick.Horizontal;

    public void Initialize()
    {
        if (_floatingJoystick == null)
        {
            Debug.LogWarning("No Joystick is assigned on InputController!");
        }
    }
    public Vector3 GetMovementVector()
    {
        _movementVector.x = HorizontalInput;
        _movementVector.y = 0f;
        _movementVector.z = VerticalInput;
        return _movementVector;
    }
}
