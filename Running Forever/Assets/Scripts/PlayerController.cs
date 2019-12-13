using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float jumpHeight; // Variable that defines the jump height
    private bool canJump;
    private bool canDoubleJump;

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
    }

    void DoubleJump()
    {
        canDoubleJump = false;
        rb.AddForce(new Vector2(0f, jumpHeight * 50f));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("entered");
        canJump = true;
    }
}
