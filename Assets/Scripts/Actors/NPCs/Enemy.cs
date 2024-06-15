using UnityEngine;

public class Enemy : Actor {

    [SerializeField] protected Collectible Droppable;

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
}