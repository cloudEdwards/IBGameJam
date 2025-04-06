using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float acceleration = 20f;
    public float deceleration = 20f;

    private Vector3 velocity;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get raw input (instant, no smoothing)
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(h, 0, v).normalized;

        // Accelerate toward target velocity
        Vector3 targetVelocity = inputDir * moveSpeed;
        velocity = Vector3.MoveTowards(velocity, targetVelocity, 
            (inputDir.magnitude > 0 ? acceleration : deceleration) * Time.deltaTime);

        // Move the player using CharacterController for no friction/stickiness
        controller.Move(velocity * Time.deltaTime);
    }
}
