using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private bool canJump;
    [SerializeField]
    private float jumpHeight; // Variable that defines the jump height

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            }
        }
    }

    void Jump()
    {
        canJump = false;
        rb.AddForce(new Vector3(0f, jumpHeight, 0f), ForceMode.VelocityChange);
    }

    void OnTriggerEnter(Collider other)
    {
        canJump = true;
    }
}
