using System;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour, IInitializable, IPlayerController, IPlayerControllerDataProvider
{
    [SerializeField] private PlayerEntity playerEntity;
    public Transform PlayerTransform => playerEntity.transform;

    public void Initialize()
    {
        Subscribe();
        if (playerEntity == null)
        {
            Debug.LogError("Player Entity is not assigned! Please assign.");
            return;
        }
        playerEntity.Initialize();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
    private void Subscribe()
    {
        EventController.Instance.OnLevelProceeded += OnLevelProceededHandler;
    }
    private void Unsubscribe()
    {
        EventController.Instance.OnLevelProceeded -= OnLevelProceededHandler;
    }

    private void OnLevelProceededHandler()
    {
        playerEntity.OnLevelProceeded();
    }

    public void DealDamageToPlayer(float damageAmount)
    {
        playerEntity.TakeDamage(damageAmount);
    }

    public void SetPlayableStatusOfPlayer(bool status)
    {
        playerEntity.SetPlayableStatus(status);
    }

    public void UpdateModelLookRotation(Vector3 targetRotation)
    {
        playerEntity.UpdateModelLookRotation(targetRotation);
    }
}
