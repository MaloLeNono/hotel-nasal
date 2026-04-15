using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SceneFadeIn : MonoBehaviour
{
    [SerializeField] private Image blackOverlay;
    [SerializeField] private float fadeTime;

    private void Awake()
    {
        blackOverlay.color = new Color(0, 0, 0, 1);
        blackOverlay.gameObject.SetActive(true);
    }

    private void Start() => StartCoroutine(FadeIn());

    private IEnumerator FadeIn()
    {
        float timer = 0f;
        float startAlpha = blackOverlay.color.a;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;

            float t = timer / fadeTime;

            Color color = blackOverlay.color;
            color.a = Mathf.Lerp(startAlpha, 0f, t);
            blackOverlay.color = color;

            yield return null;
        }
    }
}