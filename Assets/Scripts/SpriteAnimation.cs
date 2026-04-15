using UnityEngine;
using UnityEngine.InputSystem;

public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private ParticleSystem moneyParticles;
    
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int LastHorizontal = Animator.StringToHash("LastHorizontal");
    private static readonly int LastVertical = Animator.StringToHash("LastVertical");
    
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private InputAction _moveAction;
    private Rigidbody2D _rigidbody;
    
    private void Awake()
    {
        Wallet.Instance.money.OnMoneyChanged += DamageAnimation;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() => _moveAction = inputActions.FindAction("Move");

    private void Update()
    {
        Vector2 direction = _moveAction.ReadValue<Vector2>();

        if (Mathf.Abs(direction.x) > 0.01f)
            _spriteRenderer.flipX = direction.x < 0;

        _animator.SetFloat(Horizontal, direction.x);
        _animator.SetFloat(Vertical, direction.y);

        if (direction == Vector2.zero) return;

        _animator.SetFloat(LastHorizontal, direction.x);
        _animator.SetFloat(LastVertical, direction.y);
    }

    private void DamageAnimation(int deltaMoney)
    {
        if (deltaMoney > 0) return;
        moneyParticles.Emit(Mathf.Abs(deltaMoney));
    }
}