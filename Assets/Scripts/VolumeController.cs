using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    
    private TextMeshProUGUI _volumeIndicator;
    
    private void Awake()
    {
        _volumeIndicator = GetComponent<TextMeshProUGUI>();
        volumeSlider.value = volumeSlider.maxValue;
    }

    private void OnEnable() => volumeSlider.interactable = true;
    private void OnDisable() => volumeSlider.interactable = false;

    private void Update()
    {
        _volumeIndicator.text = $"Volume: {volumeSlider.value:N0}%";
        AudioListener.volume = volumeSlider.value / 100f;
    }
}
