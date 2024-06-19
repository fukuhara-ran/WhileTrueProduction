using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour {
    [SerializeField] protected int HealthPoint = 100;
    [SerializeField] protected int StaminaPoint = 100;
    [SerializeField] protected int Damage = 33;
    [SerializeField] protected float speed = 4f;
    [SerializeField] protected float jumpingPower = 6f;
    [SerializeField] protected float attackCD = 1f;
    
    protected ContactFilter2D contactFilter2D = new();

    protected bool isMovingRight;
    protected bool isMovingLeft;
    protected bool isFacingRight;
    protected bool isJumping;
    protected bool isAttacking;
    [NonSerialized] public UnityEvent onAttacking = new();
    protected bool isAttacked;
    protected float horizontal;
    protected Vector3 facingRight;
    protected Vector3 facingLeft;
    protected float nextAttack;

    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected Collider2D AttackCollider;
    [SerializeField] protected Animator animator;

    void Start() {
        facingRight = transform.localScale;
        facingLeft = facingRight;
        facingLeft.x *= -1;
    }

    public void Damaged(int damage) {
        HealthPoint -= damage;
        Attacked();
    }

    protected void Attack() {
        onAttacking.Invoke();
        animator.SetBool("isAttacking", true);
        List<Collider2D> actors = new();
        Physics2D.OverlapCollider(AttackCollider, contactFilter2D, actors);

        foreach(var actor in actors) {
            if(actor.GetComponent<Actor>() != null && !actor.CompareTag(tag)) {
                actor.transform.GetComponent<Actor>().Damaged(Damage);
                continue;
            }

            if(actor.GetComponent<FireBall>() != null) {
                actor.transform.GetComponent<FireBall>().Reverse();
            }
        }
    }

    protected void EndAttacking() {
        animator.SetBool("isAttacking", false);
        isAttacking = false;
    }

    protected void Attacked() {
        if(isAttacking) return;
        animator.SetBool("isAttacked", true);
        isAttacked = true;
    }

    protected void EndAttacked() {
        animator.SetBool("isAttacked", false);
        isAttacked = false;
    }

    protected void Move(float multiplier) {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    protected void FlipRight() {
        if(transform.localScale.Equals(facingLeft)) {
            transform.localScale = facingRight;
            isFacingRight = true;
        }
    }

    protected void FlipLeft() {
        if(transform.localScale.Equals(facingRight)) {
            transform.localScale = facingLeft;
            isFacingRight = false;
        }
    }

    protected bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    protected void Slide() {

    }

    protected void EndSliding() {
        animator.SetBool("isSliding", false);
    }
}
