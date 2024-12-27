using UnityEngine;

public class EnemyAnimation : HumanoidAnimationBase
{
    public static readonly int Idle = Animator.StringToHash("Idle");
    public static readonly int Walk = Animator.StringToHash("Walk");
    public static readonly int Attack = Animator.StringToHash("Attack");
    public static readonly int Die = Animator.StringToHash("Die");
    public static readonly int TakeHit = Animator.StringToHash("TakeHit");
    public override void Initialize()
    {
        base.Initialize();
    }
}
