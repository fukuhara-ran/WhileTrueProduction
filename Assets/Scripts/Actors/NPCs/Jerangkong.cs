using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Jerangkong : Enemy {
    void FixedUpdate()
    {
        if(HealthPoint < 1) {
            Die();
            return;
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
        PlayWalkAudio();
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

    private void CounterAttack() {
        if(UnityEngine.Random.Range(1,10) < 8) {
            isAttacking = true;
        }
    }
}