public class PlayerDieState : PlayerStateBase
{
    public override void EnterState(PlayerEntity player)
    {
        player.PlayerAnimation.PlayAnim(PlayerAnimation.Die);
    }
}
