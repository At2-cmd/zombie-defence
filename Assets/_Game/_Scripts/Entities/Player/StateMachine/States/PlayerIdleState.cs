using UnityEngine;
using Zenject;

public class PlayerIdleState : PlayerStateBase
{
    public override void EnterState(PlayerEntity player)
    {
        player.PlayerAnimation.PlayAnim(PlayerAnimation.Idle);
    }
    public override void UpdateState(PlayerEntity player)
    {
        if (Input.GetMouseButton(0))
        {
            if (player.InputDataProvider.GetMovementVector().magnitude < .1f) return; //If not enough force implemented.
            player.SwitchState(player.RunState);
        }
    }
}
