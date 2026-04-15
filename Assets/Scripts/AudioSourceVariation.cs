using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSourceVariation : MonoBehaviour
{
    private void Awake() => GetComponent<AudioSource>().time += Random.Range(0f, 0.5f);
}
