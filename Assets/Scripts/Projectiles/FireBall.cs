using Unity.Mathematics;
using UnityEngine;

public class FireBall : Projectile {
    public bool goRight = true;
    void OnEnable() {
        float horizontal = goRight ? 8f : -8f;
        rb.velocity = new Vector2(horizontal, 0);
    }

    void FixedUpdate() {
        rb.velocity = new Vector2(rb.velocity.x + (goRight?0.2f:-0.2f), 0);
        if(math.abs(rb.velocity.x) > 40f) EndFlying();
    }

    void EndFlying() {
        gameObject.SetActive(false);
        Destroy(this);
    }
}