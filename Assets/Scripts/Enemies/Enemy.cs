using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public int HealthPoint = 100;

    private bool isMovingRight;
    private bool isMovingLeft;
    private bool isJumping;
    private bool isAttacking;
    private bool isFacingRight = true;

    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 10f;

    public float attackCD = 1f;
    private float nextAttack;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D bc;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Damaged(int damage) {
        HealthPoint -= damage;
        Debug.Log("HP == " + damage);
    }

    void OnEnable()
    {   
    }

    void OnDisable()
    {
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
            if (IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                isJumping = false;
            }
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        Flip();

        // animator.SetBool("isMoving", isMovingRight || isMovingLeft);
        // animator.SetBool("isJumping", rb.velocity.y > 0);

        if(isAttacking && Time.time >= nextAttack) {
            nextAttack = Time.time + attackCD;
            Debug.Log("isAttacking == " + isAttacking);
            // animator.SetBool("isAttacking", true);
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
        Debug.Log("END ATTACKING");
        // animator.SetBool("isAttacking", false);
    }
}
