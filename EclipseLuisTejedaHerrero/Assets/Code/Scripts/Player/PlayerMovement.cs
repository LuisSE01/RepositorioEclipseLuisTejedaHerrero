using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float dashForce = 10f;
    public float dashCooldown = 2f;
    public float doubleTapTimeThreshold = 0.5f; // Adjust this to set the time window for double tap

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    public bool isGrounded = false;
    private bool canJump = true;
    public bool canDash = true;
    private bool isLookingRight=true;
    private float lastKeyPressTime;
    private KeyCode lastKeyCode = KeyCode.None;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        // Movement input
        float moveInput = Input.GetAxisRaw("Horizontal");
        moveDirection = new Vector2(moveInput, 0);

        // Debugging input detection
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump key pressed");
        }
         
        if (canDash && Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) DashBehaviour();

        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        MovePlayer(moveDirection.x * moveSpeed);
    }

    void MovePlayer(float horizontalMovement)
    {
        rb.velocity = new Vector2(horizontalMovement, rb.velocity.y);
        Debug.Log("Moving player");
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;
        canJump = false;
    }

    void Dash()
    {
        int dashDirection;
        if (isLookingRight) dashDirection = 1; else dashDirection = -1;
        //float dashDirection = Mathf.Sign(moveDirection.x); // Get dash direction
        rb.velocity = new Vector2(dashDirection * dashForce, rb.velocity.y);
        canDash = false;
        Debug.Log("Dashing");
        Invoke("ResetDash", dashCooldown);
    }

    void ResetDash()
    {
        canDash = true;
    }

    private void DashBehaviour()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput < -0.1f) isLookingRight = false; else isLookingRight = true;
        Debug.Log("Dash key pressed");
        Dash();
        if (Time.time - lastKeyPressTime < doubleTapTimeThreshold && lastKeyCode == (moveInput > 0 ? KeyCode.D : KeyCode.A))
        {
            Dash();
            lastKeyPressTime = 0f;
        }
        else
        {
            lastKeyPressTime = Time.time;
            lastKeyCode = moveInput > 0 ? KeyCode.D : KeyCode.A;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            isGrounded = true;
            canJump = true;
        }
    }
}