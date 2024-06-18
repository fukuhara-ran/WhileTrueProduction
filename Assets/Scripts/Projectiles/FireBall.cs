using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FireBall : Projectile {
    protected ContactFilter2D contactFilter2D = new();
    [SerializeField] protected Collider2D AttackCollider;
    [SerializeField] protected int Damage = 33;
    [SerializeField] protected float FlyTime = 4;
    public bool goRight = true;

    private float endFlyingTime;
    void OnEnable() {
        float horizontal = goRight ? 8f : -8f;
        rb.velocity = new Vector2(horizontal, 0);
        endFlyingTime = Time.time + FlyTime;
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(rb.velocity.x + (goRight?0.2f:-0.2f), 0);
        Hit();
        if(Time.time >= endFlyingTime) EndFlying();
    }

    void EndFlying() {
        gameObject.SetActive(false);
        Destroy(this);
    }

    void Hit() {
        List<Collider2D> actors = new();
        Physics2D.OverlapCollider(AttackCollider, contactFilter2D, actors);

        foreach(var actor in actors) {
            if(actor.GetComponent<Actor>() != null && !actor.CompareTag(tag)) {
                actor.transform.GetComponent<Actor>().Damaged(Damage);
                EndFlying();
            }
        }
    }

    public void Reverse() {
        rb.velocity = new Vector2(rb.velocity.x * -1, 0);
        goRight = !goRight;
    }
}