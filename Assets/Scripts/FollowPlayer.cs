using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private float moveTime;
    [SerializeField] private Transform playerTransform;
    
    private float _depthPosition;
    private Vector3 _velocity = Vector3.zero;

    private void Start() => _depthPosition = transform.position.z;

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            new Vector3(playerTransform.position.x, playerTransform.position.y, _depthPosition),
            ref _velocity,
            moveTime,
            Mathf.Infinity,
            Time.deltaTime
        );
    }
}
