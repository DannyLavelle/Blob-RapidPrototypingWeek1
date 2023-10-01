using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class JumpScript : MonoBehaviour
{
    //private Renderer objectRenderer;
    // Jump variables
    public float maxJumpPower = 10f;
    public int maxJumps = 2; // Number of jumps allowed
    private int jumpsRemaining; // Number of jumps left
    private bool isChargingJump = false;
    private float currentJumpPower = 0f;
    public GameObject Menu;
    // Surface sticking variables
    private bool CanStick = false;
    private Vector2 surfaceNormal;
    public Slider ChargeSlider;

    // Aiming variables
    private Vector2 aimDirection = Vector2.up; // Default aim direction is upward

    // Jump aim indicator variables
    public LineRenderer jumpAimIndicator; // Assign the "LineRenderer" GameObject in the Inspector
    public GameObject arrow; // Assign the "Arrow" GameObject in the Inspector
    public float aimIndicatorLength = 2f; // Length of the jump aim indicator
    public float maxStickForce = 10f;
    public Rigidbody2D rb;

  
    //Begin Test area
    public PlayerControls playerControls;
    private InputAction Look;
    private InputAction jump;
    private InputAction Stick;
    private InputAction Pause;
    bool JumpButtonPressed;
    bool leftTriggerPressed;
    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    private void OnEnable()
    {
        Look = playerControls.Player.Look;
        Look.Enable();
        jump = playerControls.Player.Jump;
        jump.Enable();
        jump.started += Jump;
        jump.canceled += JumpRelease;
        Stick = playerControls.Player.Stick;
        Stick.Enable();
        Stick.started += PressLT;
        Stick.canceled += ReleaseLT;
        Pause = playerControls.Player.Pause;
        Pause.Enable();
        Pause.performed += PauseButtonPress;
    }
    private void OnDisable()
    {
        Look.Disable();
        jump.Disable();
        jump.started -= Jump;
        jump.canceled -= JumpRelease;
        Stick.Disable();
        Stick.started -= PressLT;
        Stick.canceled -= ReleaseLT;
        Pause.Disable();
        Pause.performed -= PauseButtonPress;
       

    }
    private void Jump(InputAction.CallbackContext context)
    {
        JumpButtonPressed = true;
    }
    private void JumpRelease(InputAction.CallbackContext context)
    {
        JumpButtonPressed = false;
        JumpCharacter();
    }
    private void PressLT(InputAction.CallbackContext context)
    {
        leftTriggerPressed = true;
    }
    private void ReleaseLT(InputAction.CallbackContext context)
    {
        leftTriggerPressed = false;
    }
    private void PauseButtonPress(InputAction.CallbackContext context)
    {
        MenuController menu = Menu.GetComponent<MenuController>();
        menu.pausebuttonpressed();
    }
    //End Test area
    private void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        //objectRenderer = GetComponent<Renderer>();
        jumpsRemaining = maxJumps;
    }


    private void Update()
    {
        //if (currentJumpPower == maxJumpPower)
        //{
        //    objectRenderer.material.color = Color.red;
        //}
        //else
        //{
        //    objectRenderer.material.color = Color.white;
        //}


        // Read controller input
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");
        //bool JumpButtonPressed = Input.GetButton("Fire1");
        //bool leftTriggerPressed = Input.GetButton("Fire2");

        // Aim the jump using right analog stick
        aimDirection = Look.ReadValue<Vector2>();   
        //aimDirection = new Vector2(horizontalInput, verticalInput).normalized;

        // Jump charging
        if (JumpButtonPressed)
        {
            if (currentJumpPower < maxJumpPower)
            {
                currentJumpPower += Time.deltaTime * (maxJumpPower / 2f);
                ChargeSlider.value = Getpowerpercent();
            }
            else
            {
                currentJumpPower = maxJumpPower;
                ChargeSlider.value = Getpowerpercent();


            }
        }

        // Jumping
        //if (Input.GetButtonUp("Fire1"))
        //{
        //    JumpCharacter();
        //}

        // Surface sticking
        if (leftTriggerPressed && CanStick)
        {
            rb.velocity = Vector2.zero; // Stop all current movement

            // Limit the force to prevent being flung too forcefully
            Vector2 stickForce = -surfaceNormal * 10f;
            rb.AddForce(Vector2.ClampMagnitude(stickForce, maxStickForce), ForceMode2D.Impulse);
        }

        //// Surface sticking
        //if (leftTriggerPressed && CanStick)
        //{
        //    rb.velocity = Vector2.zero; // Stop all current movement
        //    rb.AddForce(-surfaceNormal * 10f, ForceMode2D.Impulse); // Add the impulse force
        //}

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
        switch (collision.gameObject.tag)
        {
            case "Sticky Surface":
            //Debug.Log("colliede with tag");
            CanStick = true;
            break;
            default:
            //Debug.Log("collided with unknown");
            break;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.contacts.Length > 0)
        {
            ContactPoint2D contact = collision.contacts[0];
            surfaceNormal = contact.normal;

            jumpsRemaining = maxJumps; // Reset jumps when touching the ground
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        CanStick = false;
    }

    private void JumpCharacter()
    {
        if (jumpsRemaining > 0)
        {
            
            Vector2 jumpDirection = aimDirection * currentJumpPower;
            rb.velocity = new Vector2(0f, rb.velocity.y); // Reset horizontal velocity, keep vertical velocity
            rb.AddForce(jumpDirection, ForceMode2D.Impulse);
            currentJumpPower = 0f;
            ChargeSlider.value = Getpowerpercent();
            jumpsRemaining--; // Decrement jumps
        }
    }
    public float Getpowerpercent()
    {
        float powerpercent = currentJumpPower / maxJumpPower;
        return powerpercent;
    }
}