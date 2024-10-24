using UnityEngine;

public class PlayerSounds : Sounds
{
    [SerializeField] private PlayerState _state;

    private void OnEnable()
    {
        _state.Changed += PlaySound;
    }

    private void OnDisable()
    {
        _state.Changed -= PlaySound;

    }
}
