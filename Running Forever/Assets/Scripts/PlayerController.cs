using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Rigidbody2D rb;

    [SerializeField]
    private GameObject sprite;
    [SerializeField]
    private float spinRate;

    [SerializeField]
    private float jumpHeight; // Variable that defines the jump height
    [SerializeField]
    private float maxVelocity;
    private bool canJump;
    private bool canDoubleJump;

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
        rb = GetComponent<Rigidbody2D>();
        StopPhysics();
    }

    void Update()
    {
        sprite.transform.eulerAngles += new Vector3(0f, 0f, -(spinRate * (Time.deltaTime * (DifficultyManager.Instance.difficulty / 9))));

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

        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }

    void Jump()
    {
        canJump = false;
        rb.AddForce(new Vector2(0f, jumpHeight * 100f));
        canDoubleJump = true;
        SFXManager.Instance.jump.Play();
    }

    void DoubleJump()
    {
        canDoubleJump = false;
        rb.AddForce(new Vector2(0f, jumpHeight * 100f));
        SFXManager.Instance.doubleJump.Play();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Level")
        {
            canJump = true;
        }
    }

    public void StartPhysics()
    {
        rb.WakeUp();
    }

    public void StopPhysics()
    {
        rb.Sleep();
    }
}
