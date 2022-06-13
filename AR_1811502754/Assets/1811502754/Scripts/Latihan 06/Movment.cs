using UnityEngine;

public class Movment : MonoBehaviour
{

    public CharacterController charController;
    public float movSpeed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    void Update()
    {
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(xMov, 0f, zMov).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) *Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation =Quaternion.Euler(0f, angle, 0f);

            charController.Move(direction * movSpeed * Time.deltaTime);
        }

    }
}
