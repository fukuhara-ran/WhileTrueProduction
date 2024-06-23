using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : Actor {

    [SerializeField] protected Adventurer adventurer;
    [SerializeField] public Collider2D PlayerDetectorCollision;

    protected void GetPlayer() {
        if(adventurer != null) return;
        List<Collider2D> players = new();
        Physics2D.OverlapCollider(PlayerDetectorCollision, contactFilter2D, players);
        foreach(var p in players) {
            if(p.GetComponent<Adventurer>() != null) {
                adventurer = p.transform.GetComponent<Adventurer>();
            }
        }
    }

    protected bool DetachPlayer(Action ActionBeforeDetaching) {
        if(adventurer == null) return true;

        if(math.abs(adventurer.HorizontalDistanceFrom(transform.position)) > (PlayerDetectorCollision as BoxCollider2D).size.x / 2) {
            ActionBeforeDetaching?.Invoke();
            adventurer = null;
            return true;
        }

        return false;
    }

    new private void Reset() {
        isMovingLeft = false;
        isMovingRight = false;
        // isAttacking = false;
        isAttacked = false;
        isJumping = false;
        horizontal = 0;
    }
}