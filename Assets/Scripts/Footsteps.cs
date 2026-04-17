using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Footsteps : MonoBehaviour
{
    [SerializeField] private AudioClip[] footsteps;

    private AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    public void PlayFootstep()
    {
        if (footsteps.Length == 0) return;
        AudioClip footstep = footsteps[Random.Range(0, footsteps.Length)];
        _audioSource.PlayOneShot(footstep);
    }
}
