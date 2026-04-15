using JetBrains.Annotations;
using UnityEngine;

public class MusicController : Singleton<MusicController>
{
    [SerializeField] private AudioClip baseClip;
    
    private AudioSource _audioSource;
    [CanBeNull] private AudioClip _lastClip;
    private float _lastClipTime;

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start() => SwitchSong(baseClip);

    public void SwitchSong(AudioClip newSong)
    {
        _lastClipTime = _audioSource.time;
        _audioSource.time = 0f;
        _audioSource.Stop();
        _lastClip = _audioSource.clip;
        _audioSource.clip = newSong;
        _audioSource.Play();
    }

    public void RevertSong()
    {
        _audioSource.Stop();
        _audioSource.clip = _lastClip;
        _audioSource.Play();
        _audioSource.time = _lastClipTime;
    }

    public void Stop() => _audioSource.Stop();
}