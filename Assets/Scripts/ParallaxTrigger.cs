using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ParallaxTrigger : MonoBehaviour
{
    [SerializeField] private Parallax parallax;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        parallax.activated = true;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        parallax.activated = false;
    }
}