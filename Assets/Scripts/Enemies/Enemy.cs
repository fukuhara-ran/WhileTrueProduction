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
    public int Damage = 50;

    private float horizontal;
    public float speed = 4f;
    public float jumpingPower = 4f;

    public float attackCD = 1f;
    private float nextAttack;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D bc;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private ContactFilter2D contactFilter2D = new();
    public Collider2D DetectPlayer;
    public Collider2D AttackCollider;
    private Adventurer player;

    private Vector3 facingRight;
    private Vector3 facingLeft;

    // Start is called before the first frame update
    void Start()
    {
        facingRight = transform.localScale;
        facingLeft = facingRight;
        facingLeft.x *= -1;
    }

    public void Damaged(int damage) {
        HealthPoint -= damage;
        Debug.Log("HP====" + HealthPoint);
    }

    void Dead() {
        transform.position = new Vector2(-9,0);
        Destroy(this);
    }

    void OnEnable()
    {   
    }

    void OnDisable()
    {
    }
    void Update()
    {
    }

    void FixedUpdate()
    {
        isMovingRight = false;
        isMovingLeft = false;
        horizontal = 0;

        GetPlayer();
        if(player != null) {
            if(player.transform.position.x > transform.position.x) {
                isMovingRight = true;
                isMovingLeft = false;
            } else {
                isMovingRight = false;
                isMovingLeft = true;
            }

            if(player.getIsAttacking()){
                isAttacking = true;
            }
        }

        if(isAttacking && Time.time >= nextAttack) {
            nextAttack = Time.time + attackCD;
            animator.SetBool("isAttacking", true);
            Attack();
        }

        if (isMovingRight) {
            horizontal = 1f;
            FlipRight();
        } else if (isMovingLeft) {
            horizontal = -1f;
            FlipLeft();
        }

        Move(horizontal);

        if(HealthPoint <= 0) {
            Dead();
        }

        animator.SetBool("isMoving", isMovingRight || isMovingLeft);
    }

    private void GetPlayer() {
        List<Collider2D> players = new();
        Physics2D.OverlapCollider(DetectPlayer, contactFilter2D, players);
        player = null;
        foreach(var p in players) {
            if(p.GetComponent<Adventurer>() != null) {
                player = p.transform.GetComponent<Adventurer>();
            }
        }
    }

    private void Attack() {
        List<Collider2D> enemies = new();
        Physics2D.OverlapCollider(AttackCollider, contactFilter2D, enemies);

        foreach(var enemy in enemies) {
            if(enemy.GetComponent<Adventurer>() != null) {
                enemy.transform.GetComponent<Enemy>().Damaged(Damage);
            }
        }
    }

    void EndAttacking() {
        animator.SetBool("isAttacking", false);
        isAttacking = false;
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

    private void Move(float multiplier) {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
