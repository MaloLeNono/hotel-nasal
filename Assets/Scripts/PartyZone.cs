using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Collider2D))]
public class PartyZone : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private float lightIntensity;
    [SerializeField] private float lightSaturation;
    [SerializeField] private float fadeTime;
    [SerializeField] private float hueCycleSpeed;
    
    private float _intensityVelocity;
    private bool _activated;
    private Coroutine _fadeCoroutine;
    
    private void Update()
    {
        if (!_activated)
        {
            globalLight.color = Color.white;
            return;
        }

        float targetHue = Time.time * hueCycleSpeed % 1f;
        globalLight.color = Color.HSVToRGB(targetHue, lightSaturation, 1f);
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
