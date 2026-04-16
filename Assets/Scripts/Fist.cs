using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AudioSource))]
public class Fist : Trap
{
    [Range(-1f, 1f)] [SerializeField] private float forceDirection;
    [SerializeField] private float pushForce;
    
    private static readonly int Fist1 = Animator.StringToHash("Fist");
    private Animator _animator;
    private AudioSource _audioSource;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    protected override void Activate(Rigidbody2D player)
    {
        _animator.SetTrigger(Fist1);
        _audioSource.Play();
        player.AddForce(new Vector2(forceDirection * pushForce, 0f), ForceMode2D.Impulse);
        Wallet.Instance.money.Amount -= damage;
    }
}
