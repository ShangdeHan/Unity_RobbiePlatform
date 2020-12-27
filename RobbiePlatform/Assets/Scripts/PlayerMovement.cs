using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    [Header("movement details")]
    public float speed = 8f;
    public float crouchSpeedDivisor = 3f;

    [Header("jump details")]
    public float jumpForce = 6.3f;
    public float jumpHoldForce = 1.9f;
    public float jumpHoldDuration = 0.1f;
    public float courchJumpBoot = 2.5f;

    float jumpTime;

    [Header("status")]
    public bool isCrouch;
    public bool isOnGround;
    public bool isJump;
    bool isHeadBlocked;
    bool jumpPressed;
    bool jumpHeld;
    bool crouchHeld;

    [Header("environment")]
    public float footOffset = 0.4f;
    public float headClearance = 0.5f;
    public float groundDistance = 0.2f;
    public LayerMask groundLayer;

    float xVelocity;

    //Collider size
    Vector2 colliderStandSize;
    Vector2 colliderStandOffset;
    Vector2 colliderCrouchSize;
    Vector2 colliderCrouchOffset;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        colliderStandSize = coll.size;
        colliderStandOffset = coll.offset;
        colliderCrouchSize = new Vector2(coll.size.x, coll.size.y/2f);
        colliderCrouchOffset = new Vector2(coll.offset.x, coll.offset.y / 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            jumpPressed = true;
        jumpHeld = Input.GetButton("Jump");
        crouchHeld = Input.GetButton("Crouch");
    }
    private void FixedUpdate()
    {
        if (isJump)
            jumpPressed = false;
        PhysicsCheck();
        GroundMovement();
        MidAirMovement();
    }

    void PhysicsCheck()
    {
        RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset, 0f), Vector2.down, groundDistance, groundLayer);
        RaycastHit2D rightCheck = Raycast(new Vector2(footOffset, 0f), Vector2.down, groundDistance, groundLayer);
        if (leftCheck || rightCheck)
            isOnGround = true;
        else isOnGround = false;
        RaycastHit2D headCheck = Raycast(new Vector2(0f, coll.size.y), Vector2.up, headClearance, groundLayer);
        isHeadBlocked = headCheck;
    }

    void GroundMovement()
    {
        if (crouchHeld && !isCrouch && isOnGround)
            Crouch();
        else if (!crouchHeld && isCrouch && !isHeadBlocked)
            StandUp();
        else if (!isOnGround && isCrouch)
            StandUp();
        xVelocity = Input.GetAxis("Horizontal");
        if (isCrouch)
            xVelocity /= crouchSpeedDivisor;
        rb.velocity = new Vector2(xVelocity * speed, rb.velocity.y);
        FlipDirection();
    }

    void FlipDirection()
    {
        if(xVelocity < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        if (xVelocity > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    void Crouch()
    {
        isCrouch = true;
        coll.size = colliderCrouchSize;
        coll.offset = colliderCrouchOffset;
    }

    void StandUp()
    {
        isCrouch = false;
        coll.size = colliderStandSize;
        coll.offset = colliderStandOffset;
    }

    void MidAirMovement()
    {
        if(jumpPressed && isOnGround && !isJump)
        {
            if(isCrouch && isOnGround)
            {
                StandUp();
                rb.AddForce(new Vector2(0f, courchJumpBoot), ForceMode2D.Impulse);
            }
            isOnGround = false;
            isJump = true;
            jumpTime = Time.time + jumpHoldDuration;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }else if (isJump)
        {
            if (jumpHeld)
            {
                rb.AddForce(new Vector2(0f, jumpHoldForce), ForceMode2D.Impulse);
            }
            if (jumpTime < Time.time)
                isJump = false;
        }
    }

    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDuration, float length, LayerMask layer)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDuration, length, layer);
        Color color = hit ? Color.red : Color.blue;
        Debug.DrawRay(pos+offset, rayDuration*length, color);
        return hit;
    }
}
