using UnityEngine;

public abstract class HumanoidAnimationBase : MonoBehaviour
{
	[SerializeField] protected Animator animator;
	private const float _transitionDuration = 0.25f;
	public virtual void Initialize()
	{
		if (animator == null)
		{
			Debug.LogWarning("Animator is null. Please assign!");
			return;
		}
	}
	public void PlayAnim(int animHash, float transitionTime = _transitionDuration)
	{
		animator.CrossFade(animHash, _transitionDuration);
	}
}
