using UnityEngine;

public class EnemyAnimation : HumanoidAnimationBase
{
    public static readonly int Idle = Animator.StringToHash("Idle");
    public static readonly int Run = Animator.StringToHash("Run");
    public static readonly int Attack = Animator.StringToHash("Attack");
    public override void Initialize()
    {
        base.Initialize();
    }
}
