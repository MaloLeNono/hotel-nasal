using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] protected int damage;
    public bool activated;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.CompareTag("Player")) return;
        if (!activated) return;
        Activate(other.transform.GetComponent<Rigidbody2D>());
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.transform.CompareTag("Player")) return;
        if (!activated) return;
        Activate(other.transform.GetComponent<Rigidbody2D>());
    }

    protected virtual void Activate(Rigidbody2D player) { }
}