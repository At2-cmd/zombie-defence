using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour, IInitializable
{
    [SerializeField] private PlayerEntity _playerEntity;
    public void Initialize()
    {
        if (_playerEntity == null)
        {
            Debug.LogError("Player Entity is not assigned! Please assign.");
        }
    }
}
