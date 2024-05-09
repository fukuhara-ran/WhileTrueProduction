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

    private bool isMovingRight;
    private bool isMovingLeft;
    private bool isJumping;


    private float horizontal;
    public float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

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

        DoGoRight.performed += (InputAction.CallbackContext context)=>{isMovingRight=true;};
        DoGoLeft.performed += (InputAction.CallbackContext context)=>{isMovingLeft=true;};
        DoJump.performed += (InputAction.CallbackContext context)=>{isJumping=true;};

        DoGoRight.canceled += (InputAction.CallbackContext context)=>{isMovingRight=false;};
        DoGoLeft.canceled += (InputAction.CallbackContext context)=>{isMovingLeft=false;};
        DoJump.canceled += (InputAction.CallbackContext context)=>{isJumping=false;};
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
        if(isMovingRight)
        {
            horizontal = 1f;
        } 
        else if (isMovingLeft)
        {
            horizontal = -1f;
        }
        
        if (isJumping)
        {
            if(IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x , jumpingPower);
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
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) {
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
