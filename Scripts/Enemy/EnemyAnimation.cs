using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private AnimationHolder _animation;

    public void PlayAnimation(bool isDie)
    {
        _animation.PlayDieAnimation(isDie);
    }
}
