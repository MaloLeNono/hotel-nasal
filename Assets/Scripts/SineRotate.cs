using UnityEngine;

public class SineRotate : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float amplitude;

    private float _timer;
    
    private void Update()
    {
        _timer = Mathf.Repeat(_timer + Time.deltaTime * speed, 2f * Mathf.PI);

        float zRotation = amplitude * Mathf.Sin(_timer);
        transform.rotation = Quaternion.Euler(0f, 0f, zRotation);
    }
}
