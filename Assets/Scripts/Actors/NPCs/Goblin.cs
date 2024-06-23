using Unity.Mathematics;
using UnityEngine;

public class Goblin : Enemy {
    void FixedUpdate()
    {
        if(HealthPoint < 1) {
            Die();
            isAttacking = false;
            animator.SetBool("isAttacked", false);
        }
        Reset();
        
        GetPlayer();

        if(adventurer != null) {
            float playerDistance = adventurer.HorizontalDistanceFrom(transform.position);
            if(playerDistance < -2f) {
                isMovingRight = true;
                isMovingLeft = false;
            } else if(playerDistance > 2f){
                isMovingRight = false;
                isMovingLeft = true;
            }

            if(playerDistance < 0) {
                FlipRight();
            } else if(playerDistance > 0) {
                FlipLeft();
            }

            if(Time.time >= nextAttack && !animator.GetBool("isDying") && math.abs(playerDistance) < 3f) {
                nextAttack = Time.time + attackCD;
                isAttacking = true;
            }
            
            DetachPlayer(null);
        }

        if(isAttacking) {
            Attack();
            isAttacking = false;
        }

        if (isMovingRight) {
            horizontal = 2f;
            FlipRight();
        } else if (isMovingLeft) {
            horizontal = -2f;
            FlipLeft();
        } else {
            horizontal = 0f;
        }

        if(animator.GetBool("isAttacking")) {
            isMovingLeft = false;
            isMovingRight = false;
            horizontal = 0f;
        }

        Move(horizontal);
        
        animator.SetBool("isMoving", isMovingRight || isMovingLeft);
    }
}