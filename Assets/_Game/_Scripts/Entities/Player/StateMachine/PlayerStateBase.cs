public abstract class PlayerStateBase
{
    public virtual void EnterState(PlayerEntity player) { }
    public virtual void UpdateState(PlayerEntity player) { }
    public virtual void FixedUpdateState(PlayerEntity player) { }
}