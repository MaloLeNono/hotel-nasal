using UnityEngine;

public class DebugTeleport : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            transform.position = new Vector3(251, -73, -1);
    }
}