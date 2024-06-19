using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    internal enum CheckpointState
    {
        Inactive, 
        Active 
    }
    private Animator animator;
    [SerializeField] private Collider2D playerDetect;
    private CheckpointState state = CheckpointState.Inactive;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private bool IsPlayerDetected()
    {
        List<Collider2D> colliders = new();
        Physics2D.OverlapCollider(playerDetect, new ContactFilter2D(), colliders);
        foreach(var collider in colliders) {
            if(collider.GetComponent<Adventurer>() != null) {
                Debug.Log("Player Detected");
                return true;
            }
        }
        return false;
    }
    private void Update()
    {
        switch (state)
        {
            case CheckpointState.Inactive:
                if(IsPlayerDetected())
                {
                    state = CheckpointState.Active;
                    animator.SetTrigger("Activate");
                }
                break;
            case CheckpointState.Active:
                break;
        }
    }
}
