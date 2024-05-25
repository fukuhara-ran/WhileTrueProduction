using UnityEngine;

public class AdventurerAttack : MonoBehaviour
{
    public int damage = 10;
    public Enemy enemy;

    public AdventurerMovement adventurerMovement;
    void Start()
    {
        if(adventurerMovement != null){
            adventurerMovement.OnAttacking += HandleAttack;
            Debug.Log("Subscribed to the event.");
        }
    }

    void HandleAttack()
    {
        if(enemy != null) {
            Debug.Log("ATTACK CONNECTED");
            enemy.Damaged(damage);
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        Debug.Log("Searching enemy");
        Enemy enemy = collider2D.GetComponent<Enemy>();
        if(enemy != null) {
            Debug.Log("Got enemy obj");
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        // enemy = null;
    }
}
