using UnityEngine;
using Zenject;

public class PlayerIdleState : PlayerStateBase
{
    public override void EnterState(PlayerEntity player)
    {
        Debug.Log("Idle State");
    }
    public override void UpdateState(PlayerEntity player)
    {
        if (Input.GetMouseButton(0))
        {
            if (player.InputDataProvider.GetMovementVector().magnitude < .1f) return; //If not enough force implemented.
            player.SwitchState(player.runState);
        }
    }
}
