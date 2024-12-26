using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour, IInitializable, IPlayerControllerDataProvider
{
    [SerializeField] private PlayerEntity playerEntity;
    public Transform PlayerTransform => playerEntity.transform;

    public void Initialize()
    {
        if (playerEntity == null)
        {
            Debug.LogError("Player Entity is not assigned! Please assign.");
            return;
        }
        playerEntity.Initialize();
    }
}
