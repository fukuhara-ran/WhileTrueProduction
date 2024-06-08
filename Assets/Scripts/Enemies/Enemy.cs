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

    private float horizontal;
    public float speed = 4f;
    public float jumpingPower = 4f;

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
        if(HealthPoint <= 0) {
            Dead();
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
