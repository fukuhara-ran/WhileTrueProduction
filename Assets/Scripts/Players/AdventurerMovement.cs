using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class AdventurerMovement : MonoBehaviour
{
    public Animator animator;

    public InputAction DoGoRight;
    public InputAction DoGoLeft;
    public InputAction DoJump;
    public InputAction DoAttack;

    private bool isMovingRight;
    private bool isMovingLeft;
    private bool isJumping;
    private bool isAttacking;

    public event Action OnAttacking;

    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 10f;
    private bool isFacingRight = true;
    private bool canDoubleJump = false;
    public float attackCD = 1f;
    private float nextAttack;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
        MapInput();
        EnableInput();         
    }

    void OnDisable()
    {
        DisableInput();
    }
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

        Flip();

        animator.SetBool("isMoving", isMovingRight || isMovingLeft);
        animator.SetBool("isJumping", rb.velocity.y > 0);

        if(isAttacking && Time.time >= nextAttack) {
            nextAttack = Time.time + attackCD;
            // Debug.Log("isAttacking == " + isAttacking);
            animator.SetBool("isAttacking", true);
            OnAttacking.Invoke();
            DisableInput();
        }
        
        horizontal = 0f;
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

    void EndAttacking() {
        // Debug.Log("END ATTACKING");
        animator.SetBool("isAttacking", false);
        EnableInput();
    }

    private void MapInput() {
        DoGoRight.performed += (InputAction.CallbackContext context) =>
        {
            isMovingRight = true;
        };
        DoGoRight.canceled += (InputAction.CallbackContext context) => 
        { 
            isMovingRight = false;
        };

        DoGoLeft.performed += (InputAction.CallbackContext context) =>
        {
            isMovingLeft = true;
        };
        DoGoLeft.canceled += (InputAction.CallbackContext context) => 
        { 
            isMovingLeft = false; 
        };

        DoJump.performed += (InputAction.CallbackContext context) =>
        {
            isJumping = true;
        };
        DoJump.canceled += (InputAction.CallbackContext context) => 
        { 
            isJumping = false;
        };

        DoAttack.performed += (InputAction.CallbackContext context) =>
        {
            isAttacking = true;
        };
        DoAttack.canceled += (InputAction.CallbackContext context) =>
        {
            isAttacking = false;
        };  
    }

    private void EnableInput() {
        DoGoRight.Enable();
        DoGoLeft.Enable();
        DoJump.Enable();
        DoAttack.Enable();
    }

    private void DisableInput() {
        DoGoRight.Disable();
        DoGoLeft.Disable();
        DoJump.Disable();
        DoAttack.Disable();
    }
}
