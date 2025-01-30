using System;
using UnityEngine;
using Zenject;

public class PlayerEntity : MonoBehaviour, IDamageable
{
    [Inject] IInputDataProvider _inputDataProvider;
    [Inject] IUIController _uiController;
    [Inject] IGameManager _gameManager;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerShooter playerShooter;
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private float initialPlayerHealth;

    [Header("Player State Machine")]
    private PlayerStateBase _currentState;
    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerRunState RunState = new PlayerRunState();
    public PlayerDieState DieState = new PlayerDieState();


    private float _currentPlayerHealth;
    public PlayerMovement PlayerMovement => playerMovement;
    public PlayerAnimation PlayerAnimation => playerAnimation;
    private bool _isPlayerInPlayableStatus;

    public IInputDataProvider InputDataProvider => _inputDataProvider;
    public void Initialize()
    {
        playerMovement.Initialize();
        playerShooter.Initialize(this);
        playerAnimation.Initialize();
        OnLevelProceeded();
    }

    public void OnLevelProceeded()
    {
        SetPlayableStatus(true);
        _currentState = IdleState;
        _currentState.EnterState(this);
        _currentPlayerHealth = initialPlayerHealth;
    }

    private void Update()
    {
        if (!_isPlayerInPlayableStatus) return;
        _currentState.UpdateState(this);
        playerShooter.CheckForShoot();
    }

    private void FixedUpdate()
    {
        if (!_isPlayerInPlayableStatus) return;
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
        _uiController.SetPlayerHealthBar((float)(_currentPlayerHealth / initialPlayerHealth));
        if (_currentPlayerHealth <= 0)
        {
            SwitchState(DieState);
            _gameManager.OnGameFailed();
        }
    }

    public void SetPlayableStatus(bool status)
    {
        _isPlayerInPlayableStatus = status;
    }
}
