using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseController : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private Canvas pauseCanvas;
    
    private InputAction _pauseAction;
    private InputAction _resumeAction;
    
    [HideInInspector] public bool paused;

    private void Awake()
    {
        _pauseAction = inputActions.FindAction("Pause");
        _resumeAction = inputActions.FindAction("Resume");
    }

    private void Start() => Resume();

    private void Update()
    {
        if (_pauseAction.WasPressedThisFrame())
            Pause();
        else if (_resumeAction.WasPressedThisFrame())
            Resume();
    }

    public void Resume()
    {
        inputActions.FindActionMap("Player").Enable();
        inputActions.FindActionMap("UI").Disable();
        SwitchPauseStatus(false);
    }
    
    private void Pause()
    {
        inputActions.FindActionMap("Player").Disable();
        inputActions.FindActionMap("UI").Enable();
        SwitchPauseStatus(true);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void SwitchPauseStatus(bool pause)
    {
        paused = pause;
        pauseCanvas.gameObject.SetActive(pause);
        Time.timeScale = pause
            ? 0f
            : 1f;
    }
}