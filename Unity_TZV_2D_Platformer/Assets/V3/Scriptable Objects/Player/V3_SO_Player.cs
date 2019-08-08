using UnityEngine;

[CreateAssetMenu(fileName = "V3_SO_Player", menuName = "Scriptable Objects/V3_SO_Player", order = 1)]
public class V3_SO_Player : ScriptableObject
{
    [Header("Movement Properties")]
    public bool useAcceleration = true;         // The players movement should have acceleration?
    public float speed = 11f;                   // The players speed
    public float acceleration = 65f;            // The players acceleration to speed
    public float crouchSpeedDivider = 3f;       // The players crouch speed
    public float coyoteDuration = 0.05f;        // The players coyote duration before falling
    public float maxFallSpeed = -25f;           // The players max fall speed

    [Header("Jump Properties")]
    public float jumpForce = 8.0f;              // The players initial jump force
    public float crouchJumpBoost = 2.5f;        // The players boost force when crouch jumping
    public float hangingJumpForce = 18.5f;      // The players hanging jump force
    public float jumpHoldForce = 2.3f;          // The players additive jump force
    public float jumpHoldDuration = 0.1f;       // The players addditive duration time

    [Header("Environment Check Properties")]
    public float footOffset = 0.4f;             // The players X offset for foot raycasts
    public float eyeHeight = 1.5f;              // The players height for wall check
    public float reachOffset = 0.7f;            // The players X offset for wall grabbing
    public float headClearance = 0.5f;          // The players head clearance
    public float groundDistance = 0.2f;         // The players distance to be considered on the ground
    public float grabDistance = 0.6f;           // The players reach distance for wall grabs
    public LayerMask groundLayer;               // The players layer to be used for ground

    [Header("Crouch Properties")]
    public float crouchSizeDivider = 2f;        // The players crouch divider size

    [Header("Dash Properties")]
    public float dashForce = 30f;               // The players initial dash force
    public float dashWaitTime = 1f;             // The players dash dead time

    [Header("Status Flags")]
    public bool isOnGround = false;
    public bool isJumping = false;
    public bool isHanging = false;
    public bool isCrouching = false;
    public bool isHeadBlocked = false;
    public bool isFacingRight = true;
    public bool canDash = true;
    public bool isAlive = true;

    [Header("Debug")]
    public bool drawDebugRaycasts = false;
}
