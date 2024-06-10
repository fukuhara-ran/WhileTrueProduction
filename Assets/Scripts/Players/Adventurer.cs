using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class Adventurer : MonoBehaviour
{
    public Animator animator;

    public int HealthPoint = 100;
    
    public InputAction DoGoRight;
    public InputAction DoGoLeft;
    public InputAction DoJump;
    public InputAction DoAttack;
    public InputAction DoSlide;
    public Collider2D AttackCollider;
    private ContactFilter2D contactFilter2D = new();

    public int Damage = 50;

    private bool isMovingRight;
    private bool isMovingLeft;
    private bool isJumping;
    private bool isSliding;    
    private bool isStillSliding = false;    
    private bool isAttacking;

    public bool getIsAttacking() {
        return isAttacking;
    }

    private float horizontal;
    public float speed = 4f;
    public float jumpingPower = 6f;
    private Vector3 facingRight;
    private Vector3 facingLeft;
    private bool canDoubleJump = false;
    public float attackCD = 1f;
    private float nextAttack;
    private float slideCD = 4f;
    private float nextSlide;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start() {
        facingRight = transform.localScale;
        facingLeft = facingRight;
        facingLeft.x *= -1;
    }

    void OnEnable() {
        MapInput();
        EnableInput();         
    }

    void OnDisable() {
        DisableInput();
    }
    void Update() {
        
    }

    void FixedUpdate() {
        if (isMovingRight) {
            horizontal = 1f;
            FlipRight();
            
        }
        else if (isMovingLeft) {
            horizontal = -1f;
            FlipLeft();
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

        if(isAttacking && Time.time >= nextAttack) {
            nextAttack = Time.time + attackCD;
            animator.SetBool("isAttacking", isAttacking);
            Attack();
            DisableInput();
        }

        if(isSliding && Time.time >= nextSlide) {
            nextSlide = Time.time + slideCD;
            animator.SetBool("isSliding", isSliding);
            horizontal = isMovingLeft ? -2f : 2f;
            isStillSliding = true;
            DisableInput();
        }

        Move(horizontal);

        if(!IsGrounded()) {
            if(horizontal > 0f) {
                horizontal -= 0.01f;
            }

            if(horizontal < 0f) {
                horizontal += 0.01f;
            } 
        } else {
            if(!isStillSliding) {
                horizontal = 0f;
            }
        }

        animator.SetBool("isMoving", isMovingRight || isMovingLeft);
        animator.SetBool("isJumping", rb.velocity.y > 0);
        animator.SetBool("isFalling", rb.velocity.y < 0);
    }

    private void Attack() {
        List<Collider2D> enemies = new();
        Physics2D.OverlapCollider(AttackCollider, contactFilter2D, enemies);

        int i = 0;
        foreach(var enemy in enemies) {
            if(enemy.GetComponent<Enemy>() != null) {
                enemy.transform.GetComponent<Enemy>().Damaged(Damage);
                i++;
            }
        }
        // Debug.Log("Enemies caught===="+i);
    }

    public void Damaged(int damage) {
        HealthPoint -= damage;
        Debug.Log("HP====" + HealthPoint);
    }

    private void Move(float multiplier) {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void FlipRight() {
        if(transform.localScale.Equals(facingLeft)) {
            transform.localScale = facingRight;
        }
    }
    private void FlipLeft() {
        if(transform.localScale.Equals(facingRight)) {
            transform.localScale = facingLeft;
        }
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    void EndAttacking() {
        // Debug.Log("END ATTACKING");
        animator.SetBool("isAttacking", false);
        EnableInput();
    }
    void EndSliding() {
        animator.SetBool("isSliding", false);
        isStillSliding = false;
        EnableInput();
    }

    private void MapInput() {
        DoGoRight.performed += (InputAction.CallbackContext context) => {isMovingRight = true;};
        DoGoRight.canceled += (InputAction.CallbackContext context) => {isMovingRight = false;};

        DoGoLeft.performed += (InputAction.CallbackContext context) =>{isMovingLeft = true;};
        DoGoLeft.canceled += (InputAction.CallbackContext context) => {isMovingLeft = false;};

        DoJump.performed += (InputAction.CallbackContext context) =>{isJumping = true;};
        DoJump.canceled += (InputAction.CallbackContext context) => {isJumping = false;};

        DoAttack.performed += (InputAction.CallbackContext context) =>{isAttacking = true;};
        DoAttack.canceled += (InputAction.CallbackContext context) =>{isAttacking = false;};

        DoSlide.performed += (InputAction.CallbackContext context) => {isSliding = true;};
        DoSlide.canceled += (InputAction.CallbackContext context) => {isSliding = false;};
    }

    private void EnableInput() {
        DoGoRight.Enable();
        DoGoLeft.Enable();
        DoJump.Enable();
        DoAttack.Enable();
        DoSlide.Enable();
    }

    private void DisableInput() {
        DoGoRight.Disable();
        DoGoLeft.Disable();
        DoJump.Disable();
        DoAttack.Disable();
        DoSlide.Disable();
    }
}
