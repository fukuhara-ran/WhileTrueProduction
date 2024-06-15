using UnityEngine;

public class Coin : Collectible {
    [SerializeField] private float MinX = 4f;
    [SerializeField] private float MaxX = 8f;
    [SerializeField] private float MinY = 8f;
    [SerializeField] private float MaxY = 12f;
    void OnEnable() {
        rb.velocity = new Vector2(Random.Range(MinX,MaxX), Random.Range(MinY,MaxY));
    }

    void FixedUpdate() {
        if(rb.velocity.x > 0) rb.velocity = new Vector2(rb.velocity.x - 0.01f, rb.velocity.y);
    }
}