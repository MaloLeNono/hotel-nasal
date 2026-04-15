using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class CutsceneDialog : MonoBehaviour
{
    [SerializeField] private char[] acceptedPunctation;
    [SerializeField] private float defaultTimeBetweenLetters;
    [SerializeField] private float punctuationMultiplyer;
    
    public TMP_Text text;

    public void ShowLine(string textToShow)
    {
        StopAllCoroutines();
        StartCoroutine(ShowText(textToShow));
    }

    private IEnumerator ShowText(string textLine)
    {
        text.text = textLine;
        text.maxVisibleCharacters = 0;
        float timer = 0f;
        
        for (int i = 0; i < textLine.Length; i++)
        {
            text.maxVisibleCharacters = i + 1;

            float timeToWait = acceptedPunctation.Contains(textLine[i])
                ? defaultTimeBetweenLetters * punctuationMultiplyer
                : defaultTimeBetweenLetters;
            
            while (timer < timeToWait && text.maxVisibleCharacters < text.text.Length)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            
            timer = 0f;
        }
    }
}