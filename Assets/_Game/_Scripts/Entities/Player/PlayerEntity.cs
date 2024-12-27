using UnityEngine;
using Zenject;

public class PlayerEntity : MonoBehaviour
{
    [Inject] IInputDataProvider _inputDataProvider;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAnimation playerAnimation;

    [Header("Player State Machine")]
    private PlayerStateBase _currentState;
    public PlayerIdleState IdleState = new PlayerIdleState();
    public PlayerRunState RunState = new PlayerRunState();

    public PlayerMovement PlayerMovement => playerMovement;
    public PlayerAnimation PlayerAnimation => playerAnimation;
    public IInputDataProvider InputDataProvider => _inputDataProvider;
    public void Initialize()
    {
        playerMovement.Initialize();
        playerAnimation.Initialize();
        _currentState = IdleState;
        _currentState.EnterState(this);
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
}
