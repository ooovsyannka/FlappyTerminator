using UnityEngine;

[RequireComponent(typeof(Animator))]

public class AnimationHolder : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayDieAnimation(bool isDie)
    {
        _animator.SetBool(AnimationData.Params.IsDie, isDie);
    }
}
