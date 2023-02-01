using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float STOP_TRESHOLD = 0.2f;
    [Header("Speed")]
    [SerializeField] private float MAX_SPEED = 16f;
    [SerializeField] private float SPEED_DECREASE = 0.2f;
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

    private Vector2 movementInput;
    private Rigidbody2D rb;
    private PlayerAnimationController animationController;
    private SpriteRenderer sprite;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animationController = GetComponent<PlayerAnimationController>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(Roll());
        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.y = Input.GetAxis("Vertical");
        // Timers
        if (rollCooldownTimer > 0)
            rollCooldownTimer -= Time.deltaTime;
    }


    private void FixedUpdate()
    {
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

        angle = Mathf.Abs(Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg);
        if (angle < 90)
        {
            sprite.flipX = false;
            //transform.rotation = Quaternion.Euler(0f, 0, 0f);

        }
        else
        {
            sprite.flipX = true;
            //transform.rotation = Quaternion.Euler(0f, 180, 0f);
        }
    }

    IEnumerator Roll()
    {
        if (!RollOnCooldown() && !isRolling && DirectionHeld() && canMove)
        {
            Vector2 direction = GetInputDirection();
            isRolling = true;
            animationController.SetRoll(true);
            rollCooldownTimer = ROLL_COOLDOWN + rollSpeedupTime + rollSlowdownTime;
            // TODO: add invincible frames
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
        else
            yield return null;
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
    }
}
