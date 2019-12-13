using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Rigidbody2D rb;

    [SerializeField]
    private float jumpHeight; // Variable that defines the jump height
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
    }

    void FixedUpdate()
    {
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
        rb.AddForce(new Vector2(0f, jumpHeight * 50f));
        SFXManager.Instance.doubleJump.Play();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        canJump = true;
    }
}
