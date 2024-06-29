using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour {
    [SerializeField] public int FullHealthPoint = 100;
    [SerializeField] public int HealthPoint = 100;
    [SerializeField] protected float StaminaPoint = 100;
    [SerializeField] protected int Damage = 33;
    [SerializeField] protected float speed = 4f;
    [SerializeField] protected float jumpingPower = 6f;
    [SerializeField] protected float attackCD = 1f;
    
    protected ContactFilter2D contactFilter2D = new();

    protected bool isMovingRight;
    protected bool isMovingLeft;
    protected bool isFacingRight;
    protected bool isJumping;
    protected bool isAttacking;
    [NonSerialized] public UnityEvent onAttacking = new();
    protected bool isAttacked;
    [SerializeField] protected float horizontal = 0f;
    protected Vector3 facingRight;
    protected Vector3 facingLeft;
    protected float nextAttack;
    protected bool IsGroundedBefore;

    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected Collider2D AttackCollider;
    [SerializeField] protected Animator animator;

    [SerializeField] protected AudioSource WalkAudioSource;
    [SerializeField] protected AudioSource MainAudioSource;
    [SerializeField] protected AudioSource SecondaryAudioSource;
    protected AudioSource GlobalAudioSource;
    [SerializeField] protected AudioClip AttackSFX;
    [SerializeField] protected AudioClip AttackedSFX;
    [SerializeField] protected AudioClip DieSFX;
    [SerializeField] protected AudioClip JumpSFX;
    [SerializeField] protected AudioClip FallSFX;
    [SerializeField] protected AudioClip DroppableSFX;

    [SerializeField] protected Collectible Droppable;
    [SerializeField] protected int DropAmount = 1;


    protected void PlayWalkAudio(bool playForever=false) {
        if(((isMovingRight || isMovingLeft) && IsGrounded()) || playForever) {
            WalkAudioSource.enabled = true;
        } else {
            WalkAudioSource.enabled = false;
        }
    }

    protected void PlayAudio(AudioClip audioClip) {
        if(!MainAudioSource.isPlaying) {
            // Debug.Log(tag+" MainAudioSource is playing");
            MainAudioSource.clip = audioClip;
            MainAudioSource.Play();
        } else if(!SecondaryAudioSource.isPlaying){
            // Debug.Log(tag+" SecondaryAudioSource is playing");
            SecondaryAudioSource.clip = audioClip;
            SecondaryAudioSource.Play();
        } else {
            // Debug.Log(tag+" Too many audio play at the same time");
        }
    }
    
    protected void PlayAudioGlobally(AudioClip audioClip) {
        Debug.Log(tag+" GlobalAudioSource is playing");
        GlobalAudioSource.clip = audioClip;
        GlobalAudioSource.Play();
    }

    void Start() {
        facingRight = transform.localScale;
        facingLeft = facingRight;
        facingLeft.x *= -1;

        GlobalAudioSource = GameObject.Find("Audio Manager").GetComponent<AudioManager>().SfxSource;
        GlobalAudioSource.volume = SaveManager.GetInstance().ReadPref("SFXVolume");
        MainAudioSource.volume = SaveManager.GetInstance().ReadPref("SFXVolume");
        SecondaryAudioSource.volume = SaveManager.GetInstance().ReadPref("SFXVolume");

        HealthPoint = FullHealthPoint;
    }

    public void Damaged(int damage) {
        HealthPoint -= damage;
        Attacked();
    }

    protected void Attack() {
        onAttacking.Invoke();
        animator.SetBool("isAttacking", true);
    }

    protected void EndAttacking() {
        List<Collider2D> actors = new();
        Physics2D.OverlapCollider(AttackCollider, contactFilter2D, actors);

        foreach(var actor in actors) {
            if(actor.GetComponent<Actor>() != null && !actor.CompareTag(tag)) {
                actor.transform.GetComponent<Actor>().Damaged(Damage);
                continue;
            }
        }

        animator.SetBool("isAttacking", false);
        PlayAudio(AttackSFX);
        isAttacking = false;
    }

    protected void Attacked() {
        if(isAttacking) return;
        animator.SetBool("isAttacked", true);

        PlayAudio(AttackedSFX);

        isAttacked = true;
    }

    protected void EndAttacked() {
        animator.SetBool("isAttacked", false);
        isAttacked = false;
    }

    protected void Die() {
        if(!animator.GetBool("isDying")) PlayAudio(DieSFX);
        animator.SetBool("isDying", true);
    }

    protected void EndDying() {
        animator.SetBool("isDying", false);

        if(DropAmount < 1) DropAmount = UnityEngine.Random.Range(4,10);

        if(this is Adventurer) DropAmount = (this as Adventurer).Wealth;

        if(Droppable != null) {
            for(var i = 0; i < DropAmount; i++) Instantiate(Droppable, transform.position, Quaternion.identity);
        }

        PlayAudioGlobally(DroppableSFX);

        gameObject.SetActive(false);
        Destroy(this);
    }

    protected void Move(float multiplier) {
        rb.velocity = new Vector2(multiplier * speed, rb.velocity.y);
    }

    protected void FlipRight() {
        if(transform.localScale.Equals(facingLeft)) {
            transform.localScale = facingRight;
            isFacingRight = true;
        }
    }

    protected void FlipLeft() {
        if(transform.localScale.Equals(facingRight)) {
            transform.localScale = facingLeft;
            isFacingRight = false;
        }
    }

    protected bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    protected void Reset() {
        isMovingLeft = false;
        isMovingRight = false;
        isAttacking = false;
        isAttacked = false;
        isJumping = false;
        horizontal = 0;
    }
}
