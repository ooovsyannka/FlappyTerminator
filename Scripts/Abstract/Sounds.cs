using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public abstract class Sounds : MonoBehaviour
{
    [System.Serializable]
    public class AudioClipHolder
    {
        public State State;
        public AudioClip Sound;
    }

    [SerializeField] private List<AudioClipHolder> _audioClips = new List<AudioClipHolder>();

    private AudioSource _audioSource;
    private Dictionary<State, AudioClip> _soundHolder;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();   
        _soundHolder = new Dictionary<State, AudioClip>();
    }

    private void Start()
    {

        foreach (AudioClipHolder audioClip in _audioClips)
        {
            _soundHolder.Add(audioClip.State, audioClip.Sound);
        }
    }

    public void PlaySound(State state)
    {
        if (_soundHolder.ContainsKey(state))
        {
            _audioSource.clip = _soundHolder[state];

            _audioSource.PlayOneShot(_audioSource.clip);
        }
        else
        {
            _audioSource.Pause();
        }
    }
}
