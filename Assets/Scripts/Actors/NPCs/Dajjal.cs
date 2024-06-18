using Unity.Mathematics;
using UnityEngine;

public class Dajjal : Enemy {
    [SerializeField] private Projectile projectile;
    void OnEnable () {
        attackCD = 3f;
    }
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
            if(playerDistance < -18f) {
                isMovingRight = true;
                isMovingLeft = false;
            } else if(playerDistance > 18f){
                isMovingRight = false;
                isMovingLeft = true;
            }

            if(playerDistance < 0) {
                FlipRight();
            } else if(playerDistance > 0) {
                FlipLeft();
            }

            if(math.abs(playerDistance) > 20f) {
                adventurer = null;
            }
            isAttacking = true;
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

        // animator.SetBool("isMoving", isMovingRight || isMovingLeft);
    }

    new private void Attack() {
        projectile.GetComponent<FireBall>().goRight = isFacingRight;
        Instantiate(projectile, transform.position, Quaternion.identity);
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