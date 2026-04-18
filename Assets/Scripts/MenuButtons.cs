using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private float fadeTime;
    [SerializeField] private Image overlay;
    [SerializeField] private AudioSource music;
    
    public void Play()
    {
        overlay.gameObject.SetActive(true);
        StartCoroutine(FadeAndExecute(() =>
        {
            SceneManager.LoadScene("Game"); 
        }));
    }

    public void Credits()
    {
        overlay.gameObject.SetActive(true);
        StartCoroutine(FadeAndExecute(() =>
        {
            SceneManager.LoadScene("Credits"); 
        }));
    }

    public void MoreGames() => Application.OpenURL("https://saint-paul-studio.itch.io/");

    public void Quit()
    {
        overlay.gameObject.SetActive(true);
        StartCoroutine(FadeAndExecute(() =>
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }));
    }

    private IEnumerator FadeAndExecute(Action action)
    {
        float startAlpha = overlay.color.a;
        float startVolume = music.volume;

        float time = 0f;

        while (time < fadeTime)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / fadeTime);
            
            Color color = overlay.color;
            color.a = Mathf.Lerp(startAlpha, 1f, t);
            overlay.color = color;
            
            music.volume = Mathf.Lerp(startVolume, 0f, t);

            yield return null;
        }

        action?.Invoke();
    }
}
