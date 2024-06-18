using Unity.Mathematics;
using UnityEngine;

public class Dajjal : Enemy {
    [SerializeField] private Projectile projectile;
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
        }

        if(Time.time >= nextAttack) {
            nextAttack = Time.time + attackCD;
            isAttacking = true;
        }

        if(isAttacking) {
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
    }

    new private void Attack() {
        onAttacking.Invoke();
        animator.SetBool("isAttacking", true);
    }

    new private void EndAttacking() {
        animator.SetBool("isAttacking", false);
        projectile.GetComponent<FireBall>().goRight = isFacingRight;
        Instantiate(projectile,
                    new Vector3(transform.position.x + (isFacingRight ? 2:-2), transform.position.y),
                    Quaternion.Euler(new Vector3(0, 0, isFacingRight?90:-90)));
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