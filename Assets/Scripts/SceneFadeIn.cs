using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneFadeIn : MonoBehaviour
{
    [SerializeField] private Image blackOverlay;
    [SerializeField] private float smoothTime;

    private void Awake()
    {
        blackOverlay.color = new Color(0, 0, 0, 1);
        blackOverlay.gameObject.SetActive(true);
    }

    private void Start() => StartCoroutine(FadeIn());

    private IEnumerator FadeIn()
    {
        float alpha = 1f;
        float velocity = 0f;

        while (alpha > 0.001f)
        {
            alpha = Mathf.SmoothDamp(alpha, 0f, ref velocity, smoothTime);

            Color color = blackOverlay.color;
            color.a = alpha;
            blackOverlay.color = color;

            yield return null;
        }
        
        Color finalColor = blackOverlay.color;
        finalColor.a = 0f;
        blackOverlay.color = finalColor;
    }
}