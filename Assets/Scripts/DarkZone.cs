using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(BoxCollider2D), typeof(AudioSource))]
public class DarkZone : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private Light2D flashlight;
    [SerializeField] private float fadeTime;
    [SerializeField] private AudioClip music;
    
    private float _velocity;
    private float _flashlightVelocity;
    private Coroutine _fadeCoroutine;
    private AudioSource _powerDown;

    private void Awake() => _powerDown = GetComponent<AudioSource>();

    private IEnumerator FadeEffect(float intensity)
    {
        while (Mathf.Abs(intensity - globalLight.intensity) > 0.001f &&
               Mathf.Abs(flashlight.intensity - (1f - intensity)) > 0.001f)
        {
            globalLight.intensity = Mathf.SmoothDamp(
                globalLight.intensity,
                intensity,
                ref _velocity,
                fadeTime,
                Mathf.Infinity,
                Time.deltaTime
            );

            flashlight.intensity = Mathf.SmoothDamp(
                flashlight.intensity,
                1f - intensity,
                ref _flashlightVelocity,
                fadeTime,
                Mathf.Infinity,
                Time.deltaTime
            );

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (_fadeCoroutine is not null)
            StopCoroutine(_fadeCoroutine);
        MusicController.Instance.SwitchSong(music);
        _powerDown.Play();
        _fadeCoroutine = StartCoroutine(FadeEffect(0f));
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (_fadeCoroutine is not null)
            StopCoroutine(_fadeCoroutine);
        MusicController.Instance.RevertSong();
        _fadeCoroutine = StartCoroutine(FadeEffect(1f));
    }
}
