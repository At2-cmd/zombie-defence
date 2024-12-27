using UnityEngine;
using Zenject;

public class PlayerRunState : PlayerStateBase
{
    public override void EnterState(PlayerEntity player)
    {
        Debug.Log("Running State");
        player.PlayerAnimation.PlayAnim(PlayerAnimation.Run);
    }

    public override void FixedUpdateState(PlayerEntity player)
    {
        player.PlayerMovement.HandleRigidBodyMovement(player.InputDataProvider.GetMovementVector().normalized);
    }

    public override void UpdateState(PlayerEntity player)
    {
        if (Input.GetMouseButtonUp(0) || player.InputDataProvider.GetMovementVector().magnitude < .1f) //If mouse released or force is not enough
        {
            player.SwitchState(player.IdleState);
        }
        player.PlayerMovement.LookAtForwardDirection(player.InputDataProvider.GetMovementVector().normalized);
    }
}
