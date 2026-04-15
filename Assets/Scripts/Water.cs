using UnityEngine;

public class Water : Trap
{
    [SerializeField] private float timeBetweenDamage;
    [SerializeField] private float slowReferenceSpeed;
    [SerializeField] private PlayerController playerController;
    
    private AudioSource _zapSound;
    private float _timer;

    private void Awake() => _zapSound = GetComponent<AudioSource>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || !activated) return;
        playerController.UpdateSpeed(slowReferenceSpeed);
        _zapSound.Play();
        Wallet.Instance.money.Amount -= damage;
        _timer = 0f;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || !activated) return;
        playerController.UpdateSpeed(playerController.referenceSpeed);
        _zapSound.Stop();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || !activated) return;
        _timer += Time.deltaTime;

        if (_timer < timeBetweenDamage) return;
        Wallet.Instance.money.Amount -= damage;
        _timer = 0f;
    }
}
