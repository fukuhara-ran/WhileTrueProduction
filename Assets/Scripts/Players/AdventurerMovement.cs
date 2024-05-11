using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class AdventurerMovement : MonoBehaviour
{
    public InputAction DoGoRight;
    public InputAction DoGoLeft;
    public InputAction DoJump;
    public InputAction DoAttack;

    private bool isMovingRight;
    private bool isMovingLeft;
    private bool isJumping;
    private bool isAttacking;


    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 6f;
    private bool isFacingRight = true;
    private bool canDoubleJump = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
        DoGoRight.Enable();
        DoGoLeft.Enable();
        DoJump.Enable();
        DoAttack.Enable();

        DoGoRight.performed += (InputAction.CallbackContext context) =>
        {
            isMovingRight = true;
            Debug.Log("Right Performed");
        };
        DoGoLeft.performed += (InputAction.CallbackContext context) =>
        {
            isMovingLeft = true;
            Debug.Log("Left Performed");
        };
        DoJump.performed += (InputAction.CallbackContext context) =>
        {
            isJumping = true;
            Debug.Log("Jump Performed");
        };
        DoAttack.performed += (InputAction.CallbackContext context) =>
        {
            isAttacking = true;
            Debug.Log("Attack Performed");
        };

        DoGoRight.canceled += (InputAction.CallbackContext context) => 
        { 
            isMovingRight = false;
            Debug.Log("Right Cancelled");
        };
        DoGoLeft.canceled += (InputAction.CallbackContext context) => 
        { 
            isMovingLeft = false; 
            Debug.Log("Left Cancelled");
        };
        DoJump.canceled += (InputAction.CallbackContext context) => 
        { 
            isJumping = false;
            Debug.Log("Jump Cancelled");
        };
        DoAttack.canceled += (InputAction.CallbackContext context) =>
        {
            isAttacking = false;
            Debug.Log("Attack Cancelled");
        };
    }

    void OnDisable()
    {
        DoGoRight.Disable();
        DoGoLeft.Disable();
        DoJump.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingRight)
        {
            horizontal = 1f;
        }
        else if (isMovingLeft)
        {
            horizontal = -1f;
        }

        if (isJumping)
        {
            if (IsGrounded() || canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                isJumping = false;
                canDoubleJump = !canDoubleJump;
            }
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        horizontal = 0f;

        Flip();
    }

    void FixedUpdate()
    {
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
