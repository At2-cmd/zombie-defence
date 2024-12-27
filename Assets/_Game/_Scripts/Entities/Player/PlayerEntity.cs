using UnityEngine;
using Zenject;

public class PlayerEntity : MonoBehaviour, IDamageable
{
    [Inject] IInputDataProvider _inputDataProvider;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerShooter playerShooter;
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private float initialPlayerHealth;

    [Header("Player State Machine")]
    private PlayerStateBase _currentState;
    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerRunState RunState = new PlayerRunState();


    private float _currentPlayerHealth;
    public PlayerMovement PlayerMovement => playerMovement;
    public PlayerAnimation PlayerAnimation => playerAnimation;


    public IInputDataProvider InputDataProvider => _inputDataProvider;
    public void Initialize()
    {
        playerMovement.Initialize();
        playerShooter.Initialize();
        playerAnimation.Initialize();
        _currentState = IdleState;
        _currentState.EnterState(this);
        _currentPlayerHealth = initialPlayerHealth;
    }

    private void Update()
    {
        _currentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        _currentState.FixedUpdateState(this);
    }
    public void SwitchState(PlayerStateBase newState)
    {
        _currentState = newState;
        _currentState.EnterState(this);
    }

    public void TakeDamage(float damageAmount)
    {
        _currentPlayerHealth -= damageAmount;
        Debug.Log("Player health is : " + _currentPlayerHealth);
        if (_currentPlayerHealth <= 0)
        {
            Debug.Log("PLAYER DIED!");
        }
    }

    public void DealDamage(float damageAmount)
    {

    }
}
