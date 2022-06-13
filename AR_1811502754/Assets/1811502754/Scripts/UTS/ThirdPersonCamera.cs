using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Mouse Lock")]
    bool lockCursor = true;
    float mouseSensitivity = 10.0f;
    public Transform target;
    [SerializeField] Vector2 pitchMinMax = new Vector2(-40, 40);
    float yaw = 0.0f, pitch = 0.0f;
    public float smoothing = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    [Header("Camera Collision")]
    Vector3 cameraDirection;
    float cameraDistance;
    Vector2 cameraDistanceMinMax = new Vector2(0.5f, 5.0f);
    public Transform cam;


    void Start()
    {
        cameraDirection = cam.transform.localPosition.normalized;
        cameraDistance = cameraDistanceMinMax.y;

        if (lockCursor)
        {    
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        CheckCameraOcclusionAndCollision(cam);
    }
    private void LateUpdate() {
        yaw += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, smoothing);
        transform.eulerAngles = currentRotation;

        transform.position = Vector3.MoveTowards(transform.position, target.position, 0.5f);
    }

    public void CheckCameraOcclusionAndCollision(Transform cam){
        Vector3 desitedCameraPosition = transform.TransformPoint(cameraDirection * cameraDistanceMinMax.y);
        RaycastHit hit;
        if (Physics.Linecast(transform.position, desitedCameraPosition, out hit))
        {
            cameraDistance = Mathf.Clamp(hit.distance, cameraDistanceMinMax.x, cameraDistanceMinMax.y);
        }
        else {
            cameraDistance = cameraDistanceMinMax.y;
        }
        cam.localPosition = cameraDirection * cameraDistance;
    }
}
