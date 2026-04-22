using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicVolumeController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer audioMixer;
    
    private const string MixerVolumeParam = "Music";

    private TextMeshProUGUI _volumeIndicator;

    private void Awake()
    {
        _volumeIndicator = GetComponent<TextMeshProUGUI>();
        
        if (audioMixer.GetFloat(MixerVolumeParam, out float dB))
            volumeSlider.value = DbToLinear(dB) * 100f;
    }

    private void OnEnable()
    {
        volumeSlider.interactable = true;
        if (audioMixer.GetFloat(MixerVolumeParam, out float dB))
            volumeSlider.value = DbToLinear(dB) * 100f;
    }

    private void OnDisable() => volumeSlider.interactable = false;

    private void Update()
    {
        _volumeIndicator.text = $"Musique: {volumeSlider.value:N0}%";
        
        float dB = LinearToDb(volumeSlider.value / 100f);
        audioMixer.SetFloat(MixerVolumeParam, dB);
    }
    
    private static float LinearToDb(float linear)
        => linear > 0.0001f ? Mathf.Log10(linear) * 20f : -80f;
    
    private static float DbToLinear(float dB)
        => Mathf.Pow(10f, dB / 20f);
}