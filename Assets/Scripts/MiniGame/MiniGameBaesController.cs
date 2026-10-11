using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class MiniGameBaesController : MonoBehaviour
{
    public float speed = 5f;
    public float coyoteTime = 0.1f;         // можно прыгнуть сразу после схода с края
    public float jumpBufferTime = 0.15f;    // нажатие запоминается

    public Rigidbody2D r2d;

    private bool touchingGround;
    private float lastGroundedTime = -10f;
    private float lastJumpPressedTime = -10f;
    private float moveInput;
    public Animator animator;

    [Header("Движение")]          
    public float groundAcceleration = 60f;   // как быстро набирается скорость на земле
    public float groundDeceleration = 80f;   // как быстро останавливается
    public float airControl = 0.5f;          // доля управляемости в воздухе 

    [Header("Прыжок")]
    public float maxJumpHeight = 3f;     // высота при большом прыжке
    public float minJumpHeight = 1f;     // высота при маленьком прыжке
    private bool jumpReleased;
    private bool isJumping;
    private float GetGravity() => Mathf.Abs(Physics2D.gravity.y * r2d.gravityScale);
    private float HeightToVelocity(float height) => Mathf.Sqrt(2f * GetGravity() * height);

    private void Awake()
    {
        r2d = GetComponent<Rigidbody2D>();
        r2d.freezeRotation = true;
        r2d.simulated = false;
        minJumpHeight = Mathf.Min(minJumpHeight, maxJumpHeight);
    }

    public void ResetState(Vector2 pos)
    {
        if (!r2d) r2d = GetComponent<Rigidbody2D>();
        r2d.position = pos;
        r2d.rotation = 0f;
        r2d.linearVelocity = Vector2.zero;
        r2d.angularVelocity = 0f;
        transform.SetPositionAndRotation(pos, Quaternion.identity);
    }

    private void OnEnable()
    {
        r2d.linearVelocity = Vector2.zero;
        r2d.angularVelocity = 0f;
        r2d.simulated = true;
        touchingGround = false;
        lastGroundedTime = -10f;
        lastJumpPressedTime = -10f;
        moveInput = 0f;
        jumpReleased = false;
        isJumping = false;
    }

    private void OnDisable()
    {
        touchingGround = false;
        if (r2d) r2d.simulated = false;
    }

    public void ToCrouch()
    {
        
    }

    private void Update()
    {
        moveInput = 0f;
        var kb = Keyboard.current;
        if (kb == null) return;

        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed)  moveInput = -1f;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) moveInput = 1f;

        if (kb.leftShiftKey.isPressed || kb.rightShiftKey.isPressed)
            ToCrouch();

        if (kb.spaceKey.wasReleasedThisFrame)
            jumpReleased = true;

        if (kb.spaceKey.wasPressedThisFrame)
            lastJumpPressedTime = Time.time;
    }

    private void FixedUpdate()
    {
        if (!r2d.simulated) return;

        if (touchingGround) lastGroundedTime = Time.time;

        
        float targetSpeed = moveInput * speed;
        float rate = Mathf.Abs(moveInput) > 0.01f ? groundAcceleration : groundDeceleration;
        if (!touchingGround) rate *= airControl;

        var v = r2d.linearVelocity;
        v.x = Mathf.MoveTowards(v.x, targetSpeed, rate * Time.fixedDeltaTime);
        r2d.linearVelocity = v;

        bool wantsJump = Time.time - lastJumpPressedTime <= jumpBufferTime;
        bool canJump   = Time.time - lastGroundedTime   <= coyoteTime;

        
        if (wantsJump && canJump)
        {
            r2d.linearVelocity = new Vector2(v.x, HeightToVelocity(maxJumpHeight));
            lastJumpPressedTime = -10f;
            lastGroundedTime = -10f;
            jumpReleased = false;
            isJumping = true;
        }

        
        if (isJumping && jumpReleased)
        {
            v = r2d.linearVelocity;
            float minVelocity = HeightToVelocity(minJumpHeight);
            if (v.y > minVelocity)
                r2d.linearVelocity = new Vector2(v.x, minVelocity);
            isJumping = false;
        }

        
        if (isJumping && r2d.linearVelocity.y <= 0f)
            isJumping = false;

        touchingGround = false;
    }

    private void OnCollisionEnter2D(Collision2D c) => CheckGround(c);
    private void OnCollisionStay2D(Collision2D c)  => CheckGround(c);

    private void CheckGround(Collision2D c)
    {
        if (!c.collider.CompareTag("Ground")) return;

        for (int i = 0; i < c.contactCount; i++)
        {
            if (c.GetContact(i).normal.y > 0.5f)   
            {
                touchingGround = true;
                return;
            }
        }
    }
}