using UnityEngine;

public interface IInputDataProvider
{
    float VerticalInput { get; }
    float HorizontalInput { get; }
    Vector3 GetMovementVector();
}
