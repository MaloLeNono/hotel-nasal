using UnityEngine;
using UnityEngine.InputSystem;

public class CreditsControl : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;

    private InputAction _quitAction;

    private void Awake() => _quitAction = inputActions.FindAction("Quit");

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable() => inputActions.FindActionMap("Credits").Enable();

    private void Update()
    {
        if (_quitAction.WasPressedThisFrame())
            Application.Quit();
    }
}
