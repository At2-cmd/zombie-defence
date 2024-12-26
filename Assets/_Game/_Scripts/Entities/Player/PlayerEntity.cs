using UnityEngine;
using Zenject;

public class PlayerEntity : MonoBehaviour
{
    [Inject] IInputDataProvider _inputDataProvider;
    [SerializeField] private PlayerMovement playerMovement;
    
    //State Machine
    private PlayerStateBase _currentState;
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerRunningState runState = new PlayerRunningState();

    //Component Read-Only Getters
    public PlayerMovement PlayerMovement => playerMovement;
    public IInputDataProvider InputDataProvider => _inputDataProvider;
    public void Initialize()
    {
        playerMovement.Initialize();
        _currentState = idleState;
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
