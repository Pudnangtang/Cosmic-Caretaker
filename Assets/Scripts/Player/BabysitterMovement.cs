using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabysitterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float boostedSpeed = 6f;
    private float currentSpeed;
    private float speedBoostDuration = 6f;
    private float speedBoostTimer;
    public Animator animator;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool facingRight = true; // Assuming the sprite initially faces right.

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = moveSpeed;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", Mathf.Abs(movement.x));

        // If the input is moving the player right and the player is facing left...
        if (movement.x < 0 && !facingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (movement.x > 0 && facingRight)
        {
            // ... flip the player.
            Flip();
        }

        // Handle speed boost timer
        if (speedBoostTimer > 0)
        {
            speedBoostTimer -= Time.deltaTime;
            if (speedBoostTimer <= 0)
            {
                currentSpeed = moveSpeed;
            }
        }
    }


    public void ActivateSpeedBoost()
    {
        currentSpeed = boostedSpeed;
        speedBoostTimer = speedBoostDuration;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
    }

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
