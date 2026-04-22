using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Collider2D), typeof(AudioSource))]
public class PartyZone : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private float lightIntensity;
    [SerializeField] private float lightSaturation;
    [SerializeField] private float fadeTime;
    [SerializeField] private float hueCycleSpeed;
    [SerializeField] private float musicFadeDistance;
    [SerializeField] private AudioSource music;
    
    private float _intensityVelocity;
    private bool _activated;
    private Coroutine _fadeCoroutine;
    private AudioSource _audioSource;
    private Collider2D _collider;
    private Transform _playerTransform;

    private void Awake()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _audioSource = GetComponent<AudioSource>();
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        HandleMusicFade();
        
        if (!_activated)
        {
            globalLight.color = Color.white;
            return;
        }

        float targetHue = Time.time * hueCycleSpeed % 1f;
        globalLight.color = Color.HSVToRGB(targetHue, lightSaturation, 1f);
    }

    private void HandleMusicFade()
    {
        float distance = DistanceFromPlayer();
        
        if (distance > musicFadeDistance)
        {
            _audioSource.volume = 0f;
            return;
        }
        
        float t = Mathf.InverseLerp(musicFadeDistance, 1f, distance);
        _audioSource.volume = t;
        music.volume = 1f - _audioSource.volume;
    }

    private float DistanceFromPlayer()
    {
        return Vector3.Distance(_collider.bounds.ClosestPoint(_playerTransform.position), _playerTransform.position);
    }

    private IEnumerator FadeEffect(float target)
    {
        while (Mathf.Abs(globalLight.intensity - target) > 0.001f)
        {
            globalLight.intensity = Mathf.SmoothDamp(
                globalLight.intensity,
                target,
                ref _intensityVelocity,
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
        _activated = true;
        if (_fadeCoroutine is not null)
            StopCoroutine(_fadeCoroutine);
        _fadeCoroutine = StartCoroutine(FadeEffect(lightIntensity));
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        _activated = false;
        if (_fadeCoroutine is not null)
            StopCoroutine(_fadeCoroutine);
        _fadeCoroutine = StartCoroutine(FadeEffect(1f));
    }
}
