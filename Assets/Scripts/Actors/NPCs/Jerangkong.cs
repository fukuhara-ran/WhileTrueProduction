using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Jerangkong : Enemy {
    void FixedUpdate()
    {
        if(HealthPoint < 1) {
            Die();
            isAttacking = false;
        }
        Reset();
        
        GetPlayer();

        if(adventurer != null) {
            float playerDistance = adventurer.HorizontalDistanceFrom(transform.position);
            if(playerDistance < -3f) {
                isMovingRight = true;
                isMovingLeft = false;
            } else if(playerDistance > 3f){
                isMovingRight = false;
                isMovingLeft = true;
            }
            
            DetachPlayer(() => {
                adventurer.onAttacking.RemoveListener(this.CounterAttack);
            });
        }

        if(isAttacking && Time.time >= nextAttack) {
            nextAttack = Time.time + attackCD;
            Attack();
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

    new private void GetPlayer() {
        if(adventurer != null) return;
        List<Collider2D> players = new();
        Physics2D.OverlapCollider(PlayerDetectorCollision, contactFilter2D, players);
        foreach(var p in players) {
            if(p.GetComponent<Adventurer>() != null) {
                adventurer = p.transform.GetComponent<Adventurer>();
                adventurer.onAttacking.AddListener(this.CounterAttack);
            }
        }
    }

    // new private bool DetachPlayer() {
    //     if(adventurer == null) return true;

    //     if(math.abs(adventurer.HorizontalDistanceFrom(transform.position)) > (PlayerDetectorCollision as BoxCollider2D).size.x / 2) {
    //         adventurer.onAttacking.RemoveListener(this.CounterAttack);
    //         adventurer = null;
    //         return true;
    //     }

    //     return false;
    // }

    private void CounterAttack() {
        if(UnityEngine.Random.Range(1,10) < 5) {
            isAttacking = true;
        }
    }

    private void Reset() {
        isMovingLeft = false;
        isMovingRight = false;
        // isAttacking = false;
        isAttacked = false;
        isJumping = false;
        horizontal = 0;
    }
}