using UnityEngine;

public class PlayerAnimation : HumanoidAnimationBase
{
	public static readonly int Idle = Animator.StringToHash("Idle");
	public static readonly int Run = Animator.StringToHash("Run");
	public static readonly int Die = Animator.StringToHash("Die");
	public static readonly int Velocity = Animator.StringToHash("Velocity");

	public void SetPlayerBlendTreeVelocityValue(float velocityValue)
    {
		Animator.SetFloat(Velocity, velocityValue);
    }
}
