using UnityEngine;

public class Collectible : MonoBehaviour {
    [SerializeField] protected Rigidbody2D rb;
    protected ContactFilter2D contactFilter2D = new();
    [SerializeField] protected Collider2D PlayerDetectorCollision;

    protected void Erase() {
        gameObject.SetActive(false);
        Destroy(this);
    }
}