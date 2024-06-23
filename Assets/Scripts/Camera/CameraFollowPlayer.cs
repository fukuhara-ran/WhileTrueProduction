using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {
    [SerializeField] Adventurer adventurer;

    public float YShift;
    public float XShift = 5f;

    void OnEnable() {
        adventurer = GameObject.Find("Adventurer").GetComponent<Adventurer>();
    }

    public Vector3 offset;         // Offset of the camera from the player
    public float smoothSpeed = 0.125f; // Smooth speed of the camera

    void LateUpdate()
    {
        if(adventurer.IsDestroyed()) return;
        if(adventurer.transform.position.y < -6) return;
        // Calculate desired position
        Vector3 desiredPosition = new Vector3(adventurer.transform.position.x + XShift + offset.x, transform.position.y, offset.z - 10f);
        // Smoothly interpolate to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}