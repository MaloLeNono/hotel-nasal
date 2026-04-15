using System.Collections;
using DataClasses;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] private CreditsSequence[] creditsSequences;
    [SerializeField] private AudioSource music;
    [SerializeField] private float startDelay;
    private void Start() => StartCoroutine(ShowSequence(creditsSequences[0], 0));

    private IEnumerator ShowSequence(CreditsSequence sequence, int index)
    {
        if (index == 0)
            yield return new WaitForSeconds(startDelay);
        
        while (sequence.sequenceCanvasGroup.alpha < 1)
        {
            float fadeInStep = 1f / sequence.fadeInTime * Time.deltaTime;
            sequence.sequenceCanvasGroup.alpha = Mathf.MoveTowards(sequence.sequenceCanvasGroup.alpha, 1, fadeInStep);
            yield return null;
        }
        
        yield return new WaitForSeconds(sequence.stayTime);
        
        while (sequence.sequenceCanvasGroup.alpha > 0)
        {
            
            float fadeOutStep = 1f / sequence.fadeOutTime * Time.deltaTime;
            sequence.sequenceCanvasGroup.alpha = Mathf.MoveTowards(sequence.sequenceCanvasGroup.alpha, 0, fadeOutStep);
            if (index == creditsSequences.Length - 1)
                music.volume = Mathf.MoveTowards(music.volume, 0, fadeOutStep);
                    
            yield return null;
        }

        if (index + 1 >= creditsSequences.Length)
        {
            SceneManager.LoadScene("Menu");
            yield break;
        }

        StartCoroutine(ShowSequence(creditsSequences[index + 1], index + 1));
    }
}
