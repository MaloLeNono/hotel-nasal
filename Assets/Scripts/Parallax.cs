using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float parallaxFactor;

    public bool activated;

    private Vector3 _lastTargetPosition;
    private Vector3 _firstPosition;
    
    private void Start()
    {
        _lastTargetPosition = target.position;
        _firstPosition = transform.position;
    }

    private void LateUpdate()
    {
        if (!activated)
        {
            transform.position = _firstPosition;
            _lastTargetPosition = target.position;
            return;
        }
        
        Vector3 delta = target.position - _lastTargetPosition;

        transform.position += new Vector3(delta.x * parallaxFactor, delta.y * parallaxFactor, 0f);

        _lastTargetPosition = target.position;
    }
}