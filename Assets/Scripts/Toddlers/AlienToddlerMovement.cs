using UnityEngine;

public class AlienToddlerMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float directionChangeInterval = 2f;
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private float timeSinceLastDirectionChange;

    private bool isFrozen = false;
    private float freezeDuration = 10f;
    private float freezeTimer;

    private Animator animator; // Make sure this is assigned either in the Inspector or via GetComponent in Start()

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Assign the Animator component
        ChangeDirection();
    }

    void Update()
    {
        timeSinceLastDirectionChange += Time.deltaTime;
        if (timeSinceLastDirectionChange >= directionChangeInterval)
        {
            ChangeDirection();
        }

        if (freezeTimer > 0)
        {
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0)
            {
                isFrozen = false;
                animator.SetBool("IsFrozen", false); // Update the Animator parameter to stop the idle animation
            }
        }
    }

    void FixedUpdate()
    {
        if (!isFrozen)
        {
            rb.MovePosition(rb.position + movementDirection * moveSpeed * Time.fixedDeltaTime);
            animator.SetFloat("Speed", moveSpeed); // Update the Animator's Speed parameter to the moveSpeed
        }
        else
        {
            animator.SetFloat("Speed", 0); // Set Speed to 0 to trigger the idle animation
        }
    }

    public void FreezeMovement()
    {
        isFrozen = true;
        freezeTimer = freezeDuration;
        animator.SetFloat("Speed", 0); // Set Speed to 0 to trigger the idle animation
        animator.SetBool("IsFrozen", true); // Set the IsFrozen parameter if you have a specific frozen state
    }

    private void ChangeDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        movementDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;
        timeSinceLastDirectionChange = 0f;

        animator.SetFloat("Speed", moveSpeed); // Set Speed to moveSpeed to trigger the walking animation

        // Determine the direction of the NPC and update the IsFacingRight parameter
        animator.SetBool("IsFacingRight", movementDirection.x < 0);

        // Flip the NPC sprite based on direction
        if (movementDirection.x > 0 != rb.transform.localScale.x < 0) // Check if the direction is different from the current facing direction
        {
            Flip();
        }
    }

    private void Flip()
    {
        // Multiply the NPC's x local scale by -1 to flip the sprite
        Vector3 theScale = rb.transform.localScale;
        theScale.x *= -1;
        rb.transform.localScale = theScale;
    }
}
