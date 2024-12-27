using UnityEngine;

public class PlayerAnimation : HumanoidAnimationBase
{
	public static readonly int Idle = Animator.StringToHash("Idle");
	public static readonly int Run = Animator.StringToHash("Run");

    public override void Initialize()
    {
        base.Initialize();
    }
}
