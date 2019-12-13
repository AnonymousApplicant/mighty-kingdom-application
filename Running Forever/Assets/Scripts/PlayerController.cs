using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField]
    private float gravity; // Variable that defines the gravity force

    private bool canJump;
    [SerializeField]
    private float jumpHeight; // Variable that defines the jump height

    private float velocityY; // variable for gravity velocity

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        // Update velocity Y by gravity variable
        velocityY += (Time.fixedDeltaTime * 2) * gravity;

        if (controller.isGrounded && velocityY < gravity / 2) velocityY = gravity / 2;

        controller.Move(new Vector3(0f, velocityY, 0f) * Time.deltaTime);

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (controller.isGrounded)
                {
                    Jump();
                }
            }
        }
    }

    void Jump()
    {
        canJump = false;
        float jumpVelocity = Mathf.Sqrt(-2 * gravity * (jumpHeight * 2));
        velocityY = jumpVelocity;
    }
}
