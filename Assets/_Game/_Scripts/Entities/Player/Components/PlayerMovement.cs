using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [Inject] IInputDataProvider _inputDataProvider;
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

    public void HandleRigidBodyMovement(Vector3 target)
    {
        rb.MovePosition(_transform.position + (target * movementSpeed * Time.deltaTime));
    }

    public void AdjustModelLookAt(Transform pickedTarget)
    {
        if (pickedTarget != null)
        {
            Vector3 direction = pickedTarget.position - modelTransform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, targetRotation, modelRotationSpeed * Time.deltaTime);
        }
        else
        {
            if (_inputDataProvider.GetMovementVector().sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(_inputDataProvider.GetMovementVector().normalized);
                modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, targetRotation, Time.deltaTime * modelRotationSpeed);
            }
        }
    }
}
