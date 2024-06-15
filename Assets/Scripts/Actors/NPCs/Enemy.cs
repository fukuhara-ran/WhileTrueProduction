using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor {

    [SerializeField] protected Collectible Droppable;

    [SerializeField] protected Adventurer adventurer;
    [SerializeField] protected Collider2D PlayerDetectorCollision;

    protected void Die() {
        animator.SetBool("isDying", true);
    }

    protected void EndDying() {
        Debug.Log(tag+" is Dead");
        animator.SetBool("isDying", false);

        if(Droppable != null) {
            Instantiate(Droppable, transform.position, Quaternion.identity);
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
}