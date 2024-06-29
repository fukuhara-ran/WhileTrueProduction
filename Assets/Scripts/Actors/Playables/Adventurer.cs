using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Adventurer : Actor {
    public InputAction DoGoRight;
    public InputAction DoGoLeft;
    public InputAction DoJump;
    public InputAction DoAttack;
    public InputAction DoSlide;

    public int JumpCost = 20;
    public int AttackCost = 20;
    public int SlideCost = 40;

    private int NPCLayer;
    private int PCLayer;

    private bool isSlideRequested;
    private bool isSliding;
    private float slideEndTime;
    private float slideDuration = 0.4f;
    private float slideCooldown = 1.2f;
    private float lastSlideTime;

    private bool canDoubleJump;

    public int Wealth = 0;

    void OnEnable() {
        MapInput();
        EnableInput();
        if(SaveManager.GetInstance().GetCurrentPlayer() != null && SceneManager.GetActiveScene().name == SaveManager.GetInstance().GetCurrentPlayer().Level) {
            transform.position = SaveManager.GetInstance().GetCurrentPlayer().Position;
            Wealth = SaveManager.GetInstance().GetCurrentPlayer().Gold;
        }
    }
    private void Awake() {
        NPCLayer = LayerMask.NameToLayer("NPC");
        PCLayer = LayerMask.NameToLayer("Player");
    }

    void OnDisable() {
        DisableInput();
    }

    void FixedUpdate() {

        if(HealthPoint < 1) {
            Reset();
            Die();
            DisableInput();
        }

#region movement
        if (isMovingRight) {
            horizontal = 1f;
            FlipRight();
        }
        else if (isMovingLeft) {
            horizontal = -1f;
            FlipLeft();
        }

        Move(horizontal);

        if(!IsGrounded()) {
            if(horizontal > 0f) {
                horizontal -= 0.01f;
            }

            if(horizontal < 0f) {
                horizontal += 0.01f;
            } 
        }
        else if (!isSliding) {
            horizontal = 0f;
        }
#endregion

#region jumping
        if (isJumping)
        {
            if (IsGrounded() || canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                PlayAudio(JumpSFX);
                isJumping = false;
                canDoubleJump = !canDoubleJump;
            }
        }
        if (IsGrounded()) {
            canDoubleJump = true;
            }
#endregion

#region attack
        if(isAttacking && Time.time >= nextAttack) {
            nextAttack = Time.time + attackCD;
            Attack();
            DisableInput();
        }
#endregion

#region slide
        if (isSlideRequested && canSlide()) {
            StartSlide();
        }
        if (isSliding && Time.time >= slideEndTime) {
            EndSliding();
        }

        if (isSliding){
            
        }
        else {
            
        }
#endregion

        //Animation
        animator.SetBool("isMoving", isMovingRight || isMovingLeft);
        PlayWalkAudio();

        animator.SetBool("isJumping", rb.velocity.y > 0);
        animator.SetBool("isFalling", rb.velocity.y < 0);

        if(IsGroundedBefore == false && IsGroundedBefore != IsGrounded()) {
            PlayAudio(FallSFX);
        }
        IsGroundedBefore = IsGrounded();
    }

    public void addWealth() {
        Wealth++;
    }

    public float HorizontalDistanceFrom(Vector3 position) {
        return position.x - transform.position.x;
    }

    new void EndAttacking() {
        List<Collider2D> actors = new();
        Physics2D.OverlapCollider(AttackCollider, contactFilter2D, actors);

        foreach(var actor in actors) {
            if(actor.GetComponent<Actor>() != null && !actor.CompareTag(tag)) {
                actor.transform.GetComponent<Actor>().Damaged(Damage);
                continue;
            }

            if(actor.GetComponent<FireBall>() != null) {
                actor.transform.GetComponent<FireBall>().Reverse();
            }
        }
        
        animator.SetBool("isAttacking", false);
        PlayAudio(AttackSFX);
        isAttacking = false;
        EnableInput();
    }

    new protected void EndDying() {
        animator.SetBool("isDying", false);

        DropAmount = Wealth;

        if(Droppable != null) {
            for(var i = 0; i < DropAmount; i++) Instantiate(Droppable, transform.position, Quaternion.identity);
        }
        PlayAudioGlobally(DroppableSFX);

        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<CapsuleCollider2D>().enabled = false;

        StartCoroutine(Respawn());
    }

    IEnumerator Respawn() {
        yield return new WaitForSeconds(6);
        gameObject.SetActive(false);
        Destroy(this);
        SaveManager.GetInstance().GoToLatestProgress();
        
    }

    private void StartSlide() {
        isSliding = true;
        isSlideRequested = false;
        slideEndTime = Time.time + slideDuration;
        lastSlideTime = Time.time;
        animator.SetBool("isSliding", true);
        horizontal = transform.localScale.x > 0 ? 2f : -2f; // Slide in facing direction
        DisableInput();
    }
    private void EndSliding() {
        isSliding = false;
        animator.SetBool("isSliding", false);
        EnableInput();
    }

    private void MapInput() {
        DoGoRight.performed += (InputAction.CallbackContext context) => {isMovingRight = true;};
        DoGoRight.canceled += (InputAction.CallbackContext context) => {isMovingRight = false;};

        DoGoLeft.performed += (InputAction.CallbackContext context) =>{isMovingLeft = true;};
        DoGoLeft.canceled += (InputAction.CallbackContext context) => {isMovingLeft = false;};

        DoJump.performed += (InputAction.CallbackContext context) =>{isJumping = true;};
        DoJump.canceled += (InputAction.CallbackContext context) => {isJumping = false;};

        DoAttack.performed += (InputAction.CallbackContext context) =>{isAttacking = true;};
        DoAttack.canceled += (InputAction.CallbackContext context) =>{isAttacking = false;};

        DoSlide.performed += (InputAction.CallbackContext context) => { isSlideRequested = true; };
        DoSlide.canceled += (InputAction.CallbackContext context) => { isSlideRequested = false; };
    }

    private void EnableInput() {
        DoGoRight.Enable();
        DoGoLeft.Enable();
        DoJump.Enable();
        DoAttack.Enable();
        DoSlide.Enable();
    }

    private void DisableInput() {
        DoGoRight.Disable();
        DoGoLeft.Disable();
        DoJump.Disable();
        DoAttack.Disable();
        DoSlide.Disable();
    }
    private bool canSlide() {
        return Time.time >= lastSlideTime + slideCooldown && !isJumping && !isAttacking && IsGrounded() && (isMovingRight || isMovingLeft);
    }
}