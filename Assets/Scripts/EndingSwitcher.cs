using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class EndingSwitcher : MonoBehaviour
{
    private const string GoodEnding = "GoodEnding";
    private const string BadEnding = "BadEnding";
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        SceneManager.LoadScene(Wallet.Instance.debt <= Wallet.Instance.money.Amount
            ? GoodEnding
            : BadEnding
        );
    }
}
