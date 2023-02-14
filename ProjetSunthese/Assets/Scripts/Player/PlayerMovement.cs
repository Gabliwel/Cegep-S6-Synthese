using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float STOP_TRESHOLD = 0.2f;
    [Header("Speed")]
    [SerializeField] private float BASE_SPEED = 4;
    [SerializeField] private Vector2 currentVelocity;
    [Header("Roll")]
    [SerializeField] private float rollSpeed = 2.2f;
    [SerializeField] private float ROLL_COOLDOWN = 0.2f;
    [SerializeField] private float rollSpeedupTime = 0.23f;
    [SerializeField] private float rollSlowdownTime = 0.18f;
    [SerializeField] private bool isRolling;
    [SerializeField] private float rollCooldownTimer;
    [SerializeField] private bool canMove = true;
    [SerializeField] private float angle;
    [Header("KnockBack")]
    [Range(1, 20)]
    [SerializeField] private float dragStopper = 8;
    [Range(0, 2)]
    [SerializeField] private float stopAtMagnitude = 1.35f;
    
    private bool isKnockBack = false;

    private Vector2 movementInput;
    private Rigidbody2D rb;
    private PlayerAnimationController animationController;
    private SpriteRenderer sprite;
    private Player player;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animationController = GetComponent<PlayerAnimationController>();
        sprite = GetComponent<SpriteRenderer>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (isKnockBack) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!RollOnCooldown() && !isRolling && DirectionHeld() && canMove)
            {
                StartCoroutine(Roll());
            }
        }
        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.y = Input.GetAxis("Vertical");
        // Timers
        if (rollCooldownTimer > 0)
            rollCooldownTimer -= Time.deltaTime;
    }


    private void FixedUpdate()
    {
        if (isKnockBack) return;

        if (!isRolling && canMove)
        {
            currentVelocity = rb.velocity;

            movementInput.Normalize();

            BuildMovement();
            AdjustRotation();

            //Animation
            animationController.SetSpeed(movementInput.sqrMagnitude);
        }

        rb.velocity = currentVelocity;
    }

    private void BuildMovement()
    {
        currentVelocity.x = movementInput.x * BASE_SPEED;
        currentVelocity.y = movementInput.y * BASE_SPEED;

        if (Mathf.Abs(movementInput.x) < STOP_TRESHOLD)
            currentVelocity.x = 0;
        if (Mathf.Abs(movementInput.y) < STOP_TRESHOLD)
            currentVelocity.y = 0;

    }

    private void AdjustRotation()
    {
        Vector3 worldPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.x -= worldPosition.x;
        mousePosition.y -= worldPosition.y;
        mousePosition.z = 0;

        sprite.flipX = Mathf.Abs(Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg) > 90;
    }

    IEnumerator Roll()
    {
        Vector2 direction = GetInputDirection();
        isRolling = true;
        animationController.SetRoll(true);
        player.AddIframes(rollSpeedupTime + rollSlowdownTime);
        rollCooldownTimer = ROLL_COOLDOWN + rollSpeedupTime + rollSlowdownTime;
        // TODO: add sound
        currentVelocity = Vector2.zero;
        currentVelocity.x += direction.x * (BASE_SPEED * rollSpeed);
        currentVelocity.y += direction.y * (BASE_SPEED * rollSpeed);
        AdjustRotation();
        yield return new WaitForSeconds(rollSpeedupTime);
        currentVelocity /= 2;
        yield return new WaitForSeconds(rollSlowdownTime);
        isRolling = false;
        animationController.SetRoll(false);
    }


    public void AddKnockBack(Vector2 difference, float force)
    {
        if (isKnockBack) return;

        isKnockBack = true;
        rb.velocity = Vector2.zero;
        rb.drag = dragStopper;

        rb.AddForce(difference * force, ForceMode2D.Impulse);
        StartCoroutine(CheckKnockBack());
    }

    private IEnumerator CheckKnockBack()
    {
        while (rb.velocity.magnitude > stopAtMagnitude)
        {
            Debug.Log(rb.velocity.magnitude);
            yield return true;
        }
        rb.velocity = Vector2.zero;
        isKnockBack = false;
        rb.drag = 0;
    }

    bool RollOnCooldown()
    {
        return rollCooldownTimer > 0;
    }

    bool DirectionHeld()
    {
        return Mathf.Abs(movementInput.magnitude) > STOP_TRESHOLD;
    }

    Vector2 GetInputDirection()
    {
        return movementInput.normalized;
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
        currentVelocity = Vector2.zero;
    }

    public bool IsRolling()
    {
        return isRolling;
    }

    public void SpeedItemPickup()
    {
        BASE_SPEED += 1;
    }
}
