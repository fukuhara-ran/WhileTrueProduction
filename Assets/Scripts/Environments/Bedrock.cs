using System.Collections.Generic;
using UnityEngine;

public class Bedrock : MonoBehaviour {
    [SerializeField] private Collider2D playerDetect;
    private void KillAnyone()
    {
        List<Collider2D> colliders = new();
        Physics2D.OverlapCollider(playerDetect, new ContactFilter2D(), colliders);
        foreach(var collider in colliders) {
            var temp = collider.GetComponent<Actor>();
            if(temp != null && temp.healthManager.CurrentHealthPoint > 0) {
                temp.Damaged(9999);
            }
        }
    }

    void OnEnable() {
        playerDetect = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate() {
        KillAnyone();
    }
} 