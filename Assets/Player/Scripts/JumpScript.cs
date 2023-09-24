using UnityEngine;

public class JumpScript : MonoBehaviour
{
    // Jump variables
    public float maxJumpPower = 10f;
    public int maxJumps = 2; // Number of jumps allowed
    private int jumpsRemaining; // Number of jumps left
    private bool isChargingJump = false;
    private float currentJumpPower = 0f;

    // Surface sticking variables
    private bool isSticking = false;
    private Vector2 surfaceNormal;

    // Aiming variables
    private Vector2 aimDirection = Vector2.up; // Default aim direction is upward

    // Jump aim indicator variables
    public LineRenderer jumpAimIndicator; // Assign the "LineRenderer" GameObject in the Inspector
    public GameObject arrow; // Assign the "Arrow" GameObject in the Inspector
    public float aimIndicatorLength = 2f; // Length of the jump aim indicator

    private Rigidbody2D rb;

    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = maxJumps;
    }

    private void Update()
    {
        // Read controller input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool rightTriggerPressed = Input.GetButton("Fire1");
        bool leftTriggerPressed = Input.GetButton("Fire2");

        // Aim the jump using right analog stick
        aimDirection = new Vector2(horizontalInput, verticalInput).normalized;

        // Jump charging
        if (rightTriggerPressed)
        {
            if (currentJumpPower < maxJumpPower)
            {
                currentJumpPower += Time.deltaTime * (maxJumpPower / 2f);
            }
            else
            {
                currentJumpPower = maxJumpPower;
            }
        }

        // Jumping
        if (Input.GetButtonUp("Fire1"))
        {
            Jump();
        }

        // Surface sticking
        if (leftTriggerPressed && isSticking)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(-surfaceNormal * 10f, ForceMode2D.Impulse);
        }

        // Update JumpAimIndicator position and rotation
        UpdateJumpAimIndicator();
    }

    private void UpdateJumpAimIndicator()
    {
        if (jumpAimIndicator != null)
        {
            Vector3 startPoint = transform.position; // Start from the character's position
            Vector3 endPoint = startPoint + new Vector3(aimDirection.x, aimDirection.y, 0f).normalized * aimIndicatorLength; // Set the length of the indicator line

            // Update the position of the JumpAimIndicator
            jumpAimIndicator.SetPosition(0, startPoint);
            jumpAimIndicator.SetPosition(1, endPoint);

            // Position and rotate the arrow at the end of the line
            if (arrow != null)
            {
                arrow.transform.position = endPoint;
                arrow.transform.rotation = Quaternion.LookRotation(Vector3.forward, endPoint - startPoint);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts.Length > 0)
        {
            ContactPoint2D contact = collision.contacts[0];
            surfaceNormal = contact.normal;
            isSticking = true;
            jumpsRemaining = maxJumps; // Reset jumps when touching the ground
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isSticking = false;
    }

    private void Jump()
    {
        if (jumpsRemaining > 0)
        {
            Vector2 jumpDirection = aimDirection * currentJumpPower;
            rb.velocity = new Vector2(0f, rb.velocity.y); // Reset horizontal velocity, keep vertical velocity
            rb.AddForce(jumpDirection, ForceMode2D.Impulse);
            currentJumpPower = 0f;
            jumpsRemaining--; // Decrement jumps
        }
    }
}
