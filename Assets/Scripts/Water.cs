using UnityEngine;

public class Water : Trap
{
    [SerializeField] private float timeBetweenDamage;
    [SerializeField] private float slowReferenceSpeed;
    [SerializeField] private PlayerController playerController;
    
    private AudioSource _zapSound;
    private float _timer;
    private static int _waterCounter;

    private void Awake() => _zapSound = GetComponent<AudioSource>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || !activated) return;
        _waterCounter++;
        playerController.UpdateSpeed(slowReferenceSpeed);
        _zapSound.Play();
        Wallet.Instance.money.Amount -= damage;
        _timer = 0f;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || !activated) return;
        _waterCounter--;
        _zapSound.Stop();
        
        if (_waterCounter != 0) return;
        playerController.UpdateSpeed(playerController.referenceSpeed);
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
