using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioSource music;
    
    private TextMeshProUGUI _volumeIndicator;
    
    private void Awake()
    {
        _volumeIndicator = GetComponent<TextMeshProUGUI>();
        volumeSlider.value = music.volume * 100f;
    }

    private void OnEnable()
    {
        volumeSlider.interactable = true;
        volumeSlider.value = music.volume * 100f;
    }

    private void OnDisable() => volumeSlider.interactable = false;

    private void Update()
    {
        _volumeIndicator.text = $"Musique: {volumeSlider.value:N0}%";
        music.volume = volumeSlider.value / 100f;
    }
}