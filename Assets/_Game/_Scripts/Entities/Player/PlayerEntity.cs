using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    public void Initialize()
    {
        playerMovement.Initialize();
    }
}
