using System;
using System.Collections;
using System.Collections.Generic;
using DataClasses;
using ScriptableObjects.Dialog;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogController : Singleton<DialogController>
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button firstResponseButton;
    [SerializeField] private Button secondResponseButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Image characterExpression;
    [SerializeField] private float defaultTimeBetweenLetters;
    [SerializeField] private float punctuationMultiplyer;
    [SerializeField] private char[] acceptedPunctuation;
    [SerializeField] private AudioSource voicelineAudio;
    [SerializeField] private AudioSource sfxAudio;
    
    [Header("Debug")]
    [SerializeField] private DialogSectionData debugDialog;
    
    private Coroutine _textCoroutine;
    private Canvas _canvas;
    private HashSet<char> _acceptedPunctuationSet;

    private InputAction _skipTextAction;
    private InputActionMap _playerMap;
    private InputActionMap _dialogMap;
    private TextMeshProUGUI _firstResponseText;
    private TextMeshProUGUI _secondResponseText;

    protected override void Awake()
    {
        base.Awake();
        _acceptedPunctuationSet = new HashSet<char>(acceptedPunctuation);
        _secondResponseText = secondResponseButton.GetComponentInChildren<TextMeshProUGUI>();
        _firstResponseText = firstResponseButton.GetComponentInChildren<TextMeshProUGUI>();
        _canvas = GetComponent<Canvas>();
        characterExpression.preserveAspect = true;
    }

    private void OnEnable()
    {
        _skipTextAction = inputActions.FindAction("SkipText");
        _playerMap = inputActions.FindActionMap("Player");
        _dialogMap = inputActions.FindActionMap("Dialog");
    }

    private void Start()
    {
        _canvas.enabled = false;
        
        if (debugDialog)
            StartDialog(debugDialog);
    }

    public void StartDialog(DialogSectionData dialogSection)
    {
        _playerMap.Disable();
        _dialogMap.Enable();
        SetVisibleButtons(false);
        
        if (dialogSection is null)
        {
            Debug.Log("Invalid Dialog, could not display");
            EndDialog();
            return;
        }

        _canvas.enabled = true;

        if (dialogSection.dialogLines.Length < 1)
        {
            Debug.Log("Dialog is empty, could not display");
            EndDialog();
            return;
        }
        
        DialogLine currentLine = dialogSection.dialogLines[0];
        DisplayLine(currentLine, dialogSection.dialogLines);
    }

    public void StartDialog(string anonymousText)
    {
        _playerMap.Disable();
        _dialogMap.Enable();
        _canvas.enabled = true;
        DisplayLine(anonymousText);
    }

    public void StartDialog(string[] anonymousText)
    {
        _playerMap.Disable();
        _dialogMap.Enable();
        characterExpression.enabled = false;
        
        if (anonymousText.Length == 0)
        {
            Debug.Log("Dialog is empty, could not display");
            EndDialog();
            return;
        }
        
        _canvas.enabled = true;
        
        DisplayLine(anonymousText[0], anonymousText);
    }
    
    public void PlaySound(AudioClip clip)
    {
        if (!clip) return;
        sfxAudio.PlayOneShot(clip);
    }

    private void DisplayLine(DialogLine line, DialogLine[] allLines)
    {
        ClearListeners();

        if (line is null || allLines is null)
        {
            Debug.Log("Invalid dialog line or lines, could not display");
            EndDialog();
            return;
        }
        
        if (line.enterLineEvent)
            line.enterLineEvent.Execute();
        
        line.action?.Invoke();

        characterExpression.sprite = line.expression;
        
        if (_textCoroutine is not null)
            StopCoroutine(_textCoroutine);

        float chosenTimeBetweenCharacters = line.timeBetweenCharacters == 0f
            ? defaultTimeBetweenLetters
            : line.timeBetweenCharacters;
        
        _textCoroutine = StartCoroutine(ShowText(line.text, chosenTimeBetweenCharacters, () => 
            {
                SetVisibleButtons(line.canRespond);
                UpdateResponses(line);
            })
        );
        
        if (line.voiceline)
        {
            voicelineAudio.clip = line.voiceline;
            voicelineAudio.Play();
        }

        if (!line.canRespond)
        {
            continueButton.onClick.AddListener(() =>
            {
                int currentLineIndex = Array.IndexOf(allLines, line);
                
                if (line.continueEvent)
                    line.continueEvent.Execute();
                
                voicelineAudio.Stop();
                
                if (currentLineIndex + 1 > allLines.Length - 1)
                {
                    EndDialog();
                    return;
                }

                line = allLines[currentLineIndex + 1];
                DisplayLine(line, allLines);
            });
        }
        else
        {
            firstResponseButton.onClick.AddListener(() => GoToResponseDialog(line.firstResponse));
            secondResponseButton.onClick.AddListener(() => GoToResponseDialog(line.secondResponse));
        }
    }

    private void DisplayLine(string lineText)
    {
        characterExpression.enabled = false;
        ClearListeners();
        
        if (_textCoroutine is not null)
            StopCoroutine(_textCoroutine);
        
        SetVisibleButtons(false);
        _textCoroutine = StartCoroutine(ShowText(lineText, defaultTimeBetweenLetters));
        
        continueButton.onClick.AddListener(EndDialog);
    }

    private void DisplayLine(string lineText, string[] allLines)
    {
        ClearListeners();

        if (lineText is null || allLines is null)
        {
            Debug.Log("Invalid dialog line or lines, could not display");
            EndDialog();
            return;
        }
        
        if (_textCoroutine is not null)
            StopCoroutine(_textCoroutine);
        
        SetVisibleButtons(false);
        _textCoroutine = StartCoroutine(ShowText(lineText, defaultTimeBetweenLetters));
        
        continueButton.onClick.AddListener(() =>
        {
            int currentLineIndex = Array.IndexOf(allLines, lineText);
            
            if (currentLineIndex + 1 > allLines.Length - 1)
            {
                characterExpression.enabled = true;
                EndDialog();
                return;
            }

            lineText = allLines[currentLineIndex + 1];
            DisplayLine(lineText, allLines);
        });
    }

    private void GoToResponseDialog(Response response)
    {
        if (response.responseEvent)
            response.responseEvent.Execute();
        
        response.action?.Invoke();
        
        if (response.keepControlsOff)
        {
            EndDialogWithNoControls();
            return;
        }

        StartDialog(response.answerDialogSection);
    }

    private void EndDialog()
    {
        characterExpression.enabled = true;
        _canvas.enabled = false;
        voicelineAudio.clip = null;
        sfxAudio.Stop();
        _playerMap.Enable();
        _dialogMap.Disable();
    }

    private void EndDialogWithNoControls()
    {
        characterExpression.enabled = true;
        _canvas.enabled = false;
        voicelineAudio.clip = null;
        sfxAudio.Stop();
    }
 
    private IEnumerator ShowText(string textLine, float timeBetweenLetters, Action onComplete = null)
    {
        text.text = textLine;
        text.maxVisibleCharacters = 0;
        float timer = 0f;
        
        for (int i = 0; i < textLine.Length; i++)
        {
            text.maxVisibleCharacters = i + 1;

            float timeToWait = _acceptedPunctuationSet.Contains(textLine[i])
                ? timeBetweenLetters * punctuationMultiplyer
                : timeBetweenLetters;
            
            while (timer < timeToWait && text.maxVisibleCharacters < text.text.Length)
            {
                timer += Time.deltaTime;

                if (_skipTextAction.IsPressed())
                {
                    text.maxVisibleCharacters = textLine.Length;
                    onComplete?.Invoke();
                    yield break;
                }
                
                yield return null;
            }
            
            timer = 0f;
        }
        
        onComplete?.Invoke();
    }

    private void SetVisibleButtons(bool hasResponses)
    {
        firstResponseButton.gameObject.SetActive(hasResponses);
        secondResponseButton.gameObject.SetActive(hasResponses);
        continueButton.gameObject.SetActive(!hasResponses);
    }

    private void UpdateResponses(DialogLine line)
    {
        if (!line.canRespond) return;
        _firstResponseText.text = line.firstResponse.responseOption;
        _secondResponseText.text = line.secondResponse.responseOption;
    }

    private void ClearListeners()
    {
        firstResponseButton.onClick.RemoveAllListeners();
        secondResponseButton.onClick.RemoveAllListeners();
        continueButton.onClick.RemoveAllListeners();
    }
}