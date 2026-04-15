using ScriptableObjects.Dialog;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerInput;
    [SerializeField] private DialogSectionData startDialog;
    
    public float referenceSpeed;
    
    private Rigidbody2D _rigidbody;
    private float _walkSpeed;
    private float _runSpeed;
    private float _currentSpeed;
    private InputAction _movementAction;
    private InputAction _runAction;
    private InputAction _walkAction;

    private void OnEnable()
    {
        playerInput.FindActionMap("Player").Enable();
    }
    
    private void OnDisable() => playerInput.FindActionMap("Player").Disable();

    private void Awake()
    {
        UpdateSpeed(referenceSpeed);
        _rigidbody = GetComponent<Rigidbody2D>();
        _movementAction = playerInput.FindAction("Move");
        _runAction = playerInput.FindAction("Run");
        _walkAction = playerInput.FindAction("Walk");
    }

    public void UpdateSpeed(float newSpeed)
    {
        _walkSpeed = newSpeed / 2f;
        _runSpeed = newSpeed * 2f;
    }

    private void Start() => DialogController.Instance.StartDialog(startDialog);

    private void FixedUpdate()
    {
        Vector2 direction = _movementAction.ReadValue<Vector2>();
        direction.Normalize();

        if (_runAction.IsPressed())
            _currentSpeed = _runSpeed;
        else if (_walkAction.IsPressed())
            _currentSpeed = _walkSpeed;
        else
            _currentSpeed = referenceSpeed;
        
        _rigidbody.AddForce(direction * _currentSpeed);
    }
}