using UnityEngine;
using UnityEngine.InputSystem;

public class Adventurer : Actor {
    public InputAction DoGoRight;
    public InputAction DoGoLeft;
    public InputAction DoJump;
    public InputAction DoAttack;
    public InputAction DoSlide;

    public int JumpCost = 20;
    public int AttackCost = 20;
    public int SlideCost = 40;

    private bool isSliding;
    private bool canDoubleJump;
    private float nextSlide;
    private float slideCD = 1f;
    private bool isStillSliding;

    void OnEnable() {
        MapInput();
        EnableInput();
    }

    void OnDisable() {
        DisableInput();
    }

    void FixedUpdate() {
        //Movement
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
        } else if(!isStillSliding){
            horizontal = 0;
        }

        //Jump
        if (isJumping)
        {
            if (IsGrounded() || canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                isJumping = false;
                canDoubleJump = !canDoubleJump;
            }
        }

        //Attack
        if(isAttacking && Time.time >= nextAttack) {
            nextAttack = Time.time + attackCD;
            animator.SetBool("isAttacking", true);
            Attack();
            DisableInput();
        }

        //Slide
        if(isSliding && Time.time >= nextSlide) {
            nextSlide = Time.time + slideCD;
            animator.SetBool("isSliding", false);
            horizontal = isMovingLeft ? -2f : 2f;
            isStillSliding = true;
            DisableInput();
        }

        //Animation
        animator.SetBool("isMoving", isMovingRight || isMovingLeft);
        animator.SetBool("isJumping", rb.velocity.y > 0);
        animator.SetBool("isFalling", rb.velocity.y < 0);
    }

    public float HorizontalDistanceFrom(Vector3 position) {
        return position.x - transform.position.x;
    }

    new void EndAttacking() {
        animator.SetBool("isAttacking", false);
        EnableInput();
    }

    new void EndSliding() {
        animator.SetBool("isSliding", false);
        isStillSliding = false;
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

        DoSlide.performed += (InputAction.CallbackContext context) => {isSliding = true;};
        DoSlide.canceled += (InputAction.CallbackContext context) => {isSliding = false;};
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
}