using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance; // variable that holds the instance for the singleton setup

    private Rigidbody2D rb; // Variable that holds the Rigidbody2D component
    private Animator animator; // Variable that holds the Animator component

    [Header("Spin/Roll Settings")]
    [SerializeField]
    [Tooltip("The sprite object of the player")]
    private GameObject sprite;
    [SerializeField]
    [Tooltip("The rate at which the sprite appears to spin")]
    private float spinRate;

    [Header("Re/Start Information")]
    [SerializeField]
    [Tooltip("The position at which the player starts a game/round at")]
    private Vector3 startPos;

    [Header("Movement Settings")]
    [SerializeField]
    [Tooltip("Variable that defines the jump height")]
    private float jumpHeight;
    [SerializeField]
    [Tooltip("Variable that defines the max velocity so players dont shoot way up into the air")]
    private float maxVelocity;
    private bool canJump; // Boolean that defines whether the player can jump or not
    private bool canDoubleJump;// Boolean that defines whether the player can double jump or not

    void Awake()
    {
        // Run singleton check/setup
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    
    // Get the rigidbody and animator component and stop player physics
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        StopPhysics();
    }

    void Update()
    {
        // Check if the game is currently playing and if so "roll" the ball/player
        if (HUDManager.Instance.isPlaying)
        {
            sprite.transform.eulerAngles += new Vector3(0f, 0f, -(spinRate * (Time.deltaTime * (DifficultyManager.Instance.difficulty / DifficultyManager.Instance.startingDifficulty))));

            // Check if the user is touching the screen, if so check if can jump or doubleJump
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
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
            }

            // Check if the rigidbodies velocity is more than the max, if so set it to max in the same direction
            if (rb.velocity.magnitude > maxVelocity)
            {
                rb.velocity = rb.velocity.normalized * maxVelocity;
            }
        }
    }

    /// <summary>
    /// Makes the player jump up, resets when player touches the ground
    /// </summary>
    void Jump()
    {
        canJump = false;
        rb.AddForce(new Vector2(0f, jumpHeight * 100f));
        canDoubleJump = true;
        SFXManager.Instance.jump.Play();
        animator.SetTrigger("jumped");
    }

    /// <summary>
    /// Makes the player jump up again whilst mid-air, reset when 1st jump occurs
    /// </summary>
    void DoubleJump()
    {
        canDoubleJump = false;
        rb.AddForce(new Vector2(0f, jumpHeight * 100f));
        SFXManager.Instance.doubleJump.Play();
    }

    // Check if the player touches the ground/level, re-enable jump capability and trigger animation
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Level")
        {
            canJump = true;
            animator.SetTrigger("landed");
        }
    }

    /// <summary>
    /// Start (wake up) the physics on the player rigidbody
    /// </summary>
    public void StartPhysics()
    {
        rb.isKinematic = false;
    }

    /// <summary>
    /// Stop (sleep) the physics on the player rigidbody
    /// </summary>
    public void StopPhysics()
    {
        rb.isKinematic = true;
        rb.velocity = new Vector2(0f, 0f);
    }

    /// <summary>
    /// Send the player to the spawn location
    /// </summary>
    public void GoToSpawn()
    {
        gameObject.transform.position = startPos;
    }
}
