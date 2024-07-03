using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Zorath : Enemy {
    
    [SerializeField] private List<Enemy> Spawnables;

    private Enemy currentBocil;

    [SerializeField] private AudioClip SummonSFX;

    void FixedUpdate()
    {
        if(healthManager.CurrentHealthPoint < 1) {
            Die();
            isAttacking = false;
            animator.SetBool("isAttacked", false);
        }
        Reset();
        
        GetPlayer();

        if(adventurer != null) {
            float playerDistance = adventurer.HorizontalDistanceFrom(transform.position);
            if(playerDistance < 10f && playerDistance > 2) {
                isMovingRight = true;
                isMovingLeft = false;
            } else if(playerDistance > -10f && playerDistance < -2){
                isMovingRight = false;
                isMovingLeft = true;
            }

            if(playerDistance < 0) {
                FlipRight();
            } else if(playerDistance > 0) {
                FlipLeft();
            }

            if(Time.time >= nextAttack && !animator.GetBool("isDying") && math.abs(playerDistance) < 2f) {
                nextAttack = Time.time + attackCD;
                isAttacking = true;
            }

            // Debug.Log(tag+" "+playerDistance+ " so that attack ==== "+isAttacking);
            
            DetachPlayer(null);
        }

        if(currentBocil == null || currentBocil.IsDestroyed() == true) {
            Spawn();
        }

        if(isAttacking) {
            Attack();
            isAttacking = false;
        }

        if (isMovingRight) {
            horizontal = 0.1f;
            FlipRight();
        } else if (isMovingLeft) {
            horizontal = -0.1f;
            FlipLeft();
        } else {
            horizontal = 0f;
        }

        if(animator.GetBool("isAttacking")) horizontal = 0;

        Move(horizontal);
        
        animator.SetBool("isMoving", isMovingRight || isMovingLeft);
    }

    private void Spawn() {
        animator.SetBool("isSpawning", true);
        PlayAudio(SummonSFX);
    }

    private void EndSpawning() {
        animator.SetBool("isSpawning", false);
        var rand = UnityEngine.Random.Range(0, Spawnables.Count-1);
        var spawnNow = Spawnables[rand];
        (spawnNow.GetComponent<Enemy>().PlayerDetectorCollision as BoxCollider2D).size = new Vector2(30f, 2f);
        currentBocil = Instantiate(spawnNow, new Vector3(transform.position.x + (isFacingRight ? 4f:-4f), transform.position.y), Quaternion.identity);
    }

    new private void Reset() {
        isMovingLeft = false;
        isMovingRight = false;
        isAttacking = false;
        isAttacked = false;
        isJumping = false;
        horizontal = 0;
    }
}