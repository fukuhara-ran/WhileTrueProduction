using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Jerangkong : Enemy {
    [SerializeField] Adventurer adventurer;
    [SerializeField] Collider2D PlayerDetectorCollision;
    void FixedUpdate()
    {
        isMovingRight = false;
        isMovingLeft = false;
        isAttacked = false;
        horizontal = 0;
        adventurer = null;

        GetPlayer();

        if(adventurer != null) {
            if(adventurer.HorizontalDistanceFrom(transform.position) < -3f) {
                isMovingRight = true;
                isMovingLeft = false;
            } else if(adventurer.HorizontalDistanceFrom(transform.position) > 3f){
                isMovingRight = false;
                isMovingLeft = true;
            }

            if(adventurer.isAttacking){
                isAttacking = true;
            }
        }

        if(isAttacking && Time.time >= nextAttack) {
            nextAttack = Time.time + attackCD;
            animator.SetBool("isAttacking", true);
            Attack();
            isAttacking = false;
        }

        if (isMovingRight) {
            horizontal = 1f;
            FlipRight();
        } else if (isMovingLeft) {
            horizontal = -1f;
            FlipLeft();
        } else {
            horizontal = 0f;
        }

        Move(horizontal);

        animator.SetBool("isMoving", isMovingRight || isMovingLeft);
    }

    private void GetPlayer() {
        List<Collider2D> players = new();
        Physics2D.OverlapCollider(PlayerDetectorCollision, contactFilter2D, players);
        foreach(var p in players) {
            if(p.GetComponent<Adventurer>() != null) {
                adventurer = p.transform.GetComponent<Adventurer>();
            }
        }
    }

    private void Reset() {
        
    }
}