using DG.Tweening;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float modelRotationSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform modelTransform;
    private Transform _transform;
    private Tweener _modelLookAtTween;
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

    public void AdjustModelLookAt(Vector3 targetLookPos, float duration ,Action onCompleteAction)
    {
        modelTransform.DOLookAt(targetLookPos, duration, AxisConstraint.Y).OnComplete(() => onCompleteAction?.Invoke());
    }
}
