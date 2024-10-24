using System.Collections;
using UnityEngine;

public class EnemySounds : Sounds
{
    [SerializeField] private EnemyState _state;
    [SerializeField] private AnimationClip _wingFlapClip;

    private void OnEnable()
    {
        _state.Changed += PlaySound;
        StartCoroutine(WingFlapSoundDelay());
    }

    private void OnDisable()
    {
        _state.Changed -= PlaySound;
    }

    private IEnumerator WingFlapSoundDelay()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_wingFlapClip.length);

        while (enabled)
        {
            yield return waitForSeconds;

            PlaySound(State.WingFlap);
        }
    }
}