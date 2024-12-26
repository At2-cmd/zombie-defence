using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour, IInitializable
{
    [SerializeField] private PlayerEntity playerEntity;
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
