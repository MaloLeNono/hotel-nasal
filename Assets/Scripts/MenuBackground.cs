using UnityEngine;

public class MenuBackground : MonoBehaviour
{
    [SerializeField] private Transform defaultBackground;
    [SerializeField] private float transitionTime;
    
    [HideInInspector] public Transform currentBackground;

    private Vector3 _velocity;

    private void Awake() => currentBackground = defaultBackground;

    private void Update()
    {
        Vector3 targetPosition = new Vector3(
            currentBackground.position.x,
            currentBackground.position.y,
            -10f
        );
        
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref _velocity,
            transitionTime,
            Mathf.Infinity,
            Time.deltaTime
        );
    }
}