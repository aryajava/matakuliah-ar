using UnityEngine;

public class CameraLookAtTarget : MonoBehaviour
{
    
    public Transform targetObject;
    public Vector3 cameraOffset;
    public float smoothFactor = 0.5f;
    public bool lookAtTarget = false;

    void Start()
    {
        cameraOffset = transform.position -targetObject.transform.position;
    }

    void LateUpdate()
    {
        Vector3 newPosition = targetObject.transform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);

        if (lookAtTarget)
        {
            transform.LookAt(targetObject);
        }
    }
}
