using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class StopMusic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) => MusicController.Instance.Stop();
}
