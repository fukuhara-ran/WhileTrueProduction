using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectible {
    [SerializeField] private float MinX = 4f;
    [SerializeField] private float MaxX = 8f;
    [SerializeField] private float MinY = 8f;
    [SerializeField] private float MaxY = 12f;
    
    void OnEnable() {
        rb.velocity = new Vector2(Random.Range(MinX,MaxX), Random.Range(MinY,MaxY));
        GlobalAudioSource = GameObject.Find("Audio Manager").GetComponent<AudioManager>().SfxSource;
    }

    void FixedUpdate() {
        if(rb.velocity.x > 0) rb.velocity = new Vector2(rb.velocity.x - 0.01f, rb.velocity.y);
        Collected();
    }

    protected void Collected() {
        List<Collider2D> players = new();
        Physics2D.OverlapCollider(PlayerDetectorCollision, contactFilter2D, players);
        foreach(var p in players) {
            if(p.GetComponent<Adventurer>() != null) {
                p.transform.GetComponent<Adventurer>().addWealth();
                Erase();
            }
        }
    }
}