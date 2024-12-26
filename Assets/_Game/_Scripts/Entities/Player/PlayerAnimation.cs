using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator; 
	private const float _transitionDuration = 0.25f;

	public static readonly int Idle = Animator.StringToHash("Idle");
	public static readonly int Run = Animator.StringToHash("Run");

	public void Initialize()
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
