using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : Actor {

    [SerializeField] protected Collectible Droppable;

    [SerializeField] protected Adventurer adventurer;
    [SerializeField] protected Collider2D PlayerDetectorCollision;
    [SerializeField] protected int DropAmount = 1;

    protected void Die() {
        animator.SetBool("isDying", true);
    }

    protected void EndDying() {
        animator.SetBool("isDying", false);

        if(DropAmount < 1) DropAmount = UnityEngine.Random.Range(4,10);

        if(Droppable != null) {
            for(var i = 0; i < DropAmount; i++) Instantiate(Droppable, transform.position, Quaternion.identity);
        }

        gameObject.SetActive(false);
        Destroy(this);
    }

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
}