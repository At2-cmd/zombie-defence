using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [Inject] IInputDataProvider _inputDataProvider;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Rigidbody rb;
    private Transform _transform;
    private Vector3 _movementVector;

    public void Initialize()
    {
        _transform = transform;
    }

    private void Update()
    {
        SetMovementVector();
        LookAtForwardDirection();
    }

    private void SetMovementVector()
    {
        _movementVector.x = _inputDataProvider.HorizontalInput;
        _movementVector.y = 0f;
        _movementVector.z = _inputDataProvider.VerticalInput;
    }
    public void FixedUpdate()
    {
        rb.MovePosition(_transform.position + _movementVector.normalized * movementSpeed * Time.deltaTime);
    }

    public void LookAtForwardDirection()
    {
        _transform.forward = Vector3.Lerp(_transform.forward, _movementVector.normalized, Time.deltaTime * rotationSpeed);
    }
}
