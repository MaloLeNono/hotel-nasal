using UnityEngine;

public class InfiniteHallway : MonoBehaviour
{
    [SerializeField] private float leftX;
    [SerializeField] private float rightX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private Transform _player;

    private void Awake() => _player = GameObject.FindWithTag("Player").transform;

    private void Update()
    {
        Vector3 playerPosition = _player.position;
        if (playerPosition.y < minY || playerPosition.y > maxY ||
            playerPosition.x < leftX - 1f || playerPosition.x > rightX + 1f) return;
        
        float width = rightX - leftX;

        Vector3 position = _player.position;
        position.x = leftX + Mathf.Repeat(position.x - leftX, width);
        _player.position = position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(
            new Vector3(leftX, minY, transform.position.z),
            new Vector3(leftX, maxY, transform.position.z)
        );
        
        Gizmos.DrawLine(
            new Vector3(rightX, minY, transform.position.z),
            new Vector3(rightX, maxY, transform.position.z)
        );

        Gizmos.DrawLine(
            new Vector3(leftX, minY, transform.position.z),
            new Vector3(rightX, minY, transform.position.z)
        );
        
        Gizmos.DrawLine(
            new Vector3(leftX, maxY, transform.position.z),
            new Vector3(rightX, maxY, transform.position.z)
        );
    }
}
