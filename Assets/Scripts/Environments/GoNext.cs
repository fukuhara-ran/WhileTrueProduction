using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GoNext : MonoBehaviour
{
    public string NextScene;
    internal enum CheckpointState
    {
        Inactive, 
        Active 
    }
    private Animator animator;
    [SerializeField] private Collider2D playerDetect;
    private CheckpointState state = CheckpointState.Inactive;
    private Adventurer adventurer = null;
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
                adventurer = collider.GetComponent<Adventurer>();
                return true;
            }
        }
        return false;
    }
    private void FixedUpdate()
    {
        switch (state)
        {
            case CheckpointState.Inactive:
                if(IsPlayerDetected())
                {
                    state = CheckpointState.Active;
                    animator.SetTrigger("Activate");
                    SceneManager.LoadScene(NextScene);
                }
                break;
            case CheckpointState.Active:
                break;
        }
    }
}
