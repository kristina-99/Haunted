using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    private Vector3 offset;
    public Transform target;
    private const float SmoothTime = 0.15f;
    private Vector3 currentVelocity = Vector3.zero;

    private void Awake()
    {
        offset = transform.position - target.position;
    }
    
    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref currentVelocity, SmoothTime);
    }
}
