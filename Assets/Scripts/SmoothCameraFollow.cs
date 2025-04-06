using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target;          // Drag your player here
    public Vector3 offset = new Vector3(0f, 10f, -10f);  // Typical top-down or 3/4 offset
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
