using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float Horizontal;
    public GameObject LiLi;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private bool isFacingRight;

    //wallslide
    private bool isWallSliding;
    private float wallSlidingSpeed = 0.2f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;


    //movement
    public float activeMovespeed;
    private float dirX = 0f;
    [SerializeField] private LayerMask jumpableGround;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    private bool isJumping;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    public float jumpPower;

    //animastion
    private enum MovementState { idle, running, jumping, falling, walljump }


    //walljump
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(10f, 20f);

    //dash
    private TrailRenderer _trailrenderer;
    [Header("Dashing")]
    public float _dashingVelocity = 5f;
    public float _dashingTime = 10f;
    private Vector2 _dashingDir;
    private bool isDashing;
    private bool canDash = true;


    //stamina
    public static float maxStamina = 99;
    public float currentStamina = 100;
    public StaminaBar staminabar;

    //doublejump
    private bool doubleJump;


    CheckPointSystem checkpointsystem;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        _trailrenderer = GetComponent<TrailRenderer>();
        currentStamina = maxStamina;
    }

    void Update()
    {
        Movement();
        UpdateAnimationUpdate();
        WallSlide();
        WallJump();
        Dashing();
        StaminaOptions();

        if(!isWallJumping)
        {
            Flip();
        }

        Horizontal = Input.GetAxisRaw("Horizontal");
    }



    private void Movement()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        if (((!IsWalled() || IsGrounded()) && !isWallJumping ))
        {
            if (currentStamina > 10 && (LiLi.transform.position.x > (CheckPointSystem.CheckPointX -1)) && dirX<0)
            {
                rb.velocity = new Vector2(dirX * activeMovespeed, rb.velocity.y);

            }
            else if (currentStamina > 10 && (LiLi.transform.position.x < (CheckPointSystem.CheckPointX + 55)) && dirX > 0)
            {
                rb.velocity = new Vector2(dirX * activeMovespeed, rb.velocity.y);

            }
            else
            {
                //rb.velocity = new Vector2(dirX * activeMovespeed/4, rb.velocity.y);
                rb.velocity = new Vector2(0,rb.velocity.y);
            }

        }


        //jump with coyote and double jump
        if (IsGrounded() && !Input.GetButton("Vertical"))
        {
            doubleJump = false;
        }

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Vertical"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping && currentStamina>10)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower) ;
            currentStamina -= 10;

            jumpBufferCounter = 0f;
            

            StartCoroutine(JumpCooldown());
        }

        if (Input.GetButtonUp("Vertical") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;


        }
        if (doubleJump && Input.GetButtonDown("Vertical") && currentStamina > 10)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            currentStamina -= 10;

            doubleJump = false;
        }

    }
    private void UpdateAnimationUpdate()
    {
        float dirX = Input.GetAxisRaw("Horizontal");

        MovementState state;

        if (IsWalled() && (dirX < 0 || dirX>0))
        {
            state = MovementState.walljump;
        }

        else if (dirX > 0f)
        {
            state = MovementState.running;
            //sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            //sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.2f && !IsWalled())
        {
            state = MovementState.falling;
        }


        anim.SetInteger("state", (int)state);


    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
        doubleJump = !doubleJump;
    }


    public bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }


    public bool IsWalled()
    {

        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
        
    }
    public void WallSlide()
    {
               
        if (IsWalled() && !IsGrounded() && Horizontal != 0f && currentStamina>0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));

        }
        else
        {
            isWallSliding = false;
        }
        if (IsWalled() && !IsGrounded())
        {
            currentStamina -= 0.2f;

        }

    }

    private void Flip()
    {
        if (isFacingRight && Horizontal > 0f || !isFacingRight && Horizontal < 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }
    private void StopWallJumping()
    {
        isWallJumping = false;
    }


    private void Dashing()
    {
        var dashInput = Input.GetButtonDown("Dash");

        if (dashInput && canDash && currentStamina>0)
        {
            currentStamina -= 20;
            isDashing = true;
            canDash = false;
            _trailrenderer.emitting = true;
            _dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (_dashingDir == Vector2.zero)
            {
                _dashingDir = new Vector2(transform.localScale.x, 0);
            }

            StartCoroutine(StopDashing());

        }

        if (isDashing)
        {
            rb.velocity = _dashingDir.normalized * _dashingVelocity;
            return;
        }
        if (IsGrounded())
        {
            canDash = true;
        }
    }
    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(_dashingTime);
        rb.velocity = new Vector2(0, 0);
        _trailrenderer.emitting = false;
        isDashing = false;

    }

    public void StaminaOptions()
    {
        staminabar.SetMaxStamina(maxStamina);
        staminabar.SetStamina(currentStamina);

        if (currentStamina < maxStamina)
        {
            currentStamina += (Time.deltaTime*5);
        }
    }
}
