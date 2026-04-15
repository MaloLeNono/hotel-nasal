using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float spinSpeed;
    
    private void Update() => transform.Rotate(transform.forward, spinSpeed * Time.deltaTime);
}
