using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value;

    private AudioSource _coinSound;

    private void Start()
    {
        _coinSound = GameObject.FindWithTag("Coins").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        _coinSound.Play();
        Wallet.Instance.money.Amount += value;
        Destroy(gameObject);
    }
}
