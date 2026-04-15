using UnityEngine;

public class Spikes : Trap
{
    private static readonly int ActivateAnim = Animator.StringToHash("Activate");
    [SerializeField] private float pushForce;
    
    private Animator _animator;
    private AudioSource _audioSource;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    protected override void Activate(Rigidbody2D player)
    {
        _audioSource.Play();
        _animator.SetTrigger(ActivateAnim);
        Wallet.Instance.money.Amount -= damage;
        Vector2 forceDirection = ((Vector2)player.GetComponent<Collider2D>().bounds.center - (Vector2)transform.position).normalized;
        player.AddForce(forceDirection * pushForce,  ForceMode2D.Impulse);
    }
}