using UnityEngine;

public class BombotTrigger : MonoBehaviour
{
    [SerializeField] private Bombot[] bombots;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        foreach (Bombot bombot in bombots)
        {
            if (bombot is not null)
                bombot.triggered = true;
        }
    }
}