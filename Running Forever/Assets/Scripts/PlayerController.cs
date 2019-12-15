using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance; // variable that holds the instance for the singleton setup

    private Rigidbody2D rb; // Variable that holds the Rigidbody2D component

    [SerializeField]
    private GameObject sprite; // The sprite object of the player
    [SerializeField]
    private float spinRate; // The rate at which the sprite appears to spin

    [SerializeField]
    private float jumpHeight; // Variable that defines the jump height
    [SerializeField]
    private float maxVelocity; // Variable that defines the max velocity so players dont shoot way up into the air
    private bool canJump; // Boolean that defines whether the player can jump or not
    private bool canDoubleJump;// Boolean that defines whether the player can double jump or not

    void Awake()
    {
        // Check if the Instance variable is not null and not 'this'
        if (Instance != null && Instance != this)
        {
            // Destroy gameObject connected to this script if Instance is already defined
            Destroy(this.gameObject);
        }
        else
        {
            // Assign 'this' to the Instance variable if Instance is null
            Instance = this;
        }
    }

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        // StopPhysics so the player wont fall yet
        StopPhysics();
    }

    void Update()
    {
        // Check if the game is currently playing
        if (HUDManager.Instance.isPlaying)
        {
            // Rotate sprite at the spin rate based on difficulty / startingDifficulty so at the beginning its 1f
            sprite.transform.eulerAngles += new Vector3(0f, 0f, -(spinRate * (Time.deltaTime * (DifficultyManager.Instance.difficulty / DifficultyManager.Instance.startingDifficulty))));

            // Check if the touchCount is greater than 0 (any amount of fingers touching screen)
            if (Input.touchCount > 0)
            {
                // Assign temp Touch variable to the first touch
                Touch touch = Input.GetTouch(0);
                // If the touch just began
                if (touch.phase == TouchPhase.Began)
                {
                    // Check if canJump else canDoubleJump, execute respective method if yes
                    if (canJump)
                    {
                        Jump();
                    }
                    else if (canDoubleJump)
                    {
                        DoubleJump();
                    }
                }
            }
            // Controls for PC play, to allow easier testing, would be removed in final APK builds
            else if (Input.GetButtonDown("Jump"))
            {
                if (canJump)
                {
                    Jump();
                }
                else if (canDoubleJump)
                {
                    DoubleJump();
                }
            }

            // Check if the rigidbodies velocity is more than the max, if so set it to max in the same direction
            if (rb.velocity.magnitude > maxVelocity)
            {
                rb.velocity = rb.velocity.normalized * maxVelocity;
            }
        }
    }

    /// <summary>
    /// Makes the player jump up
    /// </summary>
    void Jump()
    {
        // Set canJump to false
        canJump = false;
        // Add force to rigidbody based on jump height
        rb.AddForce(new Vector2(0f, jumpHeight * 100f));
        // Set canDoubleJump to true
        canDoubleJump = true;
        // Play jump SFX
        SFXManager.Instance.jump.Play();
    }

    /// <summary>
    /// Makes the player jump up again
    /// </summary>
    void DoubleJump()
    {
        // Set canDoubleJump to false
        canDoubleJump = false;
        // Add force to rigidbody based on jump height
        rb.AddForce(new Vector2(0f, jumpHeight * 100f));
        // Play doubleJump SFX
        SFXManager.Instance.doubleJump.Play();
    }

    // Trigger below player object to detect the ground
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check the collider was tagged "Level"
        if (other.tag == "Level")
        {
            // Set canJump to true
            canJump = true;
        }
    }

    /// <summary>
    /// Start (wake up) the physics on the player rigidbody
    /// </summary>
    public void StartPhysics()
    {
        rb.WakeUp();
    }

    /// <summary>
    /// Stop (sleep) the physics on the player rigidbody
    /// </summary>
    public void StopPhysics()
    {
        rb.Sleep();
    }
}
