using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Wire : Trap
{
    [SerializeField] private float pushForce;
    
    private AudioSource _audioSource;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    protected override void Activate(Rigidbody2D player)
    {
        _audioSource.Play();
        Wallet.Instance.money.Amount -= damage;
        Vector2 forceDirection = ((Vector2)player.GetComponent<Collider2D>().bounds.center - (Vector2)transform.position).normalized;
        player.AddForce(forceDirection * pushForce,  ForceMode2D.Impulse);
    }
}