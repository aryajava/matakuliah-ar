using UnityEngine;

public class FPSMovement : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] float moveSpeed = 4.0f;

    [Header("Camera Movement")]
    [SerializeField] float sensitivity = 2.0f;
    private float yaw = 0.0f, pitch = 0.0f;

    [Header("Jumping")]
    public float jumpForce = 6f;

    [Header("Drag")]
    [SerializeField] float groundDrag = 5f;
    [SerializeField] float airDrag = 1f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    private float playerHight = 1.0f;
    private Rigidbody rb;
    public bool isGrounded;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        isGrounded = Physics.Raycast(rb.transform.position, Vector3.down, playerHight * 0.5f + 0.001f);

        ControlDrag();
        if (Input.GetKey(jumpKey) && isGrounded)
        {
                // rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                // isGrounded = false;
                PlayerJump();
        }
        PlayerLook();
        Debug.Log(isGrounded);
        if (Input.GetKey(jumpKey))
            Debug.Log(jumpKey);
    }

    private void FixedUpdate() {
        PlayerMovement();
    }

    void PlayerJump(){
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        // isGrounded = false;
    }

    void ControlDrag()
    {
        if (!isGrounded) rb.drag = airDrag;
        if (isGrounded) rb.drag = groundDrag;
        
    }

    void PlayerLook()
    {
        pitch -= Input.GetAxisRaw("Mouse Y") * sensitivity;
        pitch = Mathf.Clamp(pitch, -40.0f, 40.0f);
        yaw += Input.GetAxisRaw("Mouse X") * sensitivity;
        // Camera.main.transform.localRotation = Quaternion.Euler(pitch, yaw, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(pitch, 0, 0);

        transform.localRotation =Quaternion.Euler(0f, yaw, 0f);
    }

    void PlayerMovement()
    {
        Vector2 axis = new Vector2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal")).normalized * moveSpeed;
        Vector3 forwardDirection = Quaternion.Euler(0, yaw, 0) * Vector3.forward;
        Vector3 rightDirection = Quaternion.Euler(0, 90, 0) * forwardDirection;
        Vector3 wishDirection = (forwardDirection * axis.x + rightDirection * axis.y + Vector3.up * rb.velocity.y);

        rb.velocity = wishDirection;
    }
}
