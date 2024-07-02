using Unity.Mathematics;
using UnityEngine;

public class Dajjal : Enemy {
    [SerializeField] private Projectile projectile;
    void FixedUpdate()
    {
        if(healthManager.CurrentHealthPoint < 1) {
            rb.gravityScale = 0.5f;
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
            
            if(Time.time >= nextAttack && !animator.GetBool("isDying")) {
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
            horizontal = 1f;
            FlipRight();
        } else if (isMovingLeft) {
            horizontal = -1f;
            FlipLeft();
        } else {
            horizontal = 0f;
        }

        Move(horizontal);

        PlayWalkAudio(true);
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

        PlayAudio(AttackSFX);
    }
}