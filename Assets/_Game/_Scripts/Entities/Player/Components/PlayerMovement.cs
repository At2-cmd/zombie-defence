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
    private Vector3 _modelLookDirectionVector;
    private Quaternion _targetRotation;
    public void Initialize()
    {
        _transform = transform;
    }

    public void HandleRigidBodyMovement(Vector3 target)
    {
        rb.MovePosition(_transform.position + (target * movementSpeed * _inputDataProvider.GetMovementVector().sqrMagnitude * Time.deltaTime));
    }

    public void AdjustModelLookAt(Transform pickedTarget)
    {
        if (pickedTarget != null)
        {
            _modelLookDirectionVector = pickedTarget.position - modelTransform.position;
            _targetRotation = Quaternion.LookRotation(_modelLookDirectionVector);
            modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, _targetRotation, modelRotationSpeed * Time.deltaTime);
        }
        else
        {
            if (_inputDataProvider.GetMovementVector().sqrMagnitude > 0.01f)
            {
                _targetRotation = Quaternion.LookRotation(_inputDataProvider.GetMovementVector().normalized);
                modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, _targetRotation, Time.deltaTime * modelRotationSpeed);
            }
        }
    }
}
