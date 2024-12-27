using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Rigidbody rb;
    private Transform _transform;
    public void Initialize()
    {
        _transform = transform;
    }

    public void LookAtForwardDirection(Vector3 target)
    {
        _transform.forward = Vector3.Lerp(_transform.forward, target, (Time.deltaTime * rotationSpeed));
    }

    public void HandleRigidBodyMovement(Vector3 target)
    {
        rb.MovePosition(_transform.position + (target * movementSpeed * Time.deltaTime));
    }
}
