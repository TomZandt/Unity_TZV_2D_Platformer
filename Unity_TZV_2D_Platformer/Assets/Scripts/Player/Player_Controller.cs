using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private FloatVariable playerMoveSpeed;
    [SerializeField] private FloatVariable playerMoveSmoothFactor;
    [SerializeField] private BoolVariable isPlayerFacingRight;
    [Space(10)]

    [Header("Ground")]
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private FloatVariable groundedRadius;
    [SerializeField] private BoolVariable isPlayerGrounded;
    [Space(10)]

    [Header("Jumping")]
    [SerializeField] private FloatVariable playerJumpVelocity;
    [SerializeField] private FloatVariable playerGravityMultiplier;
    [SerializeField] private FloatVariable defaultGravityScale;
    [Space(10)]

    [Header("Ladder")]
    [SerializeField] private LayerMask ladderLayerMask;
    [SerializeField] private FloatVariable playerLadderDetectDistance;
    [SerializeField] private BoolVariable isPlayerOnLadder;

    private Player_UserInput userInputObj;
    private Rigidbody2D playerRb2D;
    private Vector2 horizontalVelocity = Vector2.zero;  // Used for smoothDamp
    private Vector2 verticalVelocity = Vector2.zero;    // Used for smoothDamp

    //****************************************************************************************************
    private void Start()
    {
        // Assign the user input object
        userInputObj = GetComponent<Player_UserInput>();

        // Assign the rigid body object
        playerRb2D = GetComponent<Rigidbody2D>();
        playerRb2D.bodyType = RigidbodyType2D.Dynamic;
        playerRb2D.gravityScale = defaultGravityScale.value;
        playerRb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        playerRb2D.interpolation = RigidbodyInterpolation2D.Interpolate;
        playerRb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    //****************************************************************************************************
    private void Update()
    {
        ProcessFlip();
    }

    //****************************************************************************************************
    private void FixedUpdate()
    {
        CheckForGround();

        ProcessHorizontalMovement();

        ProcessJump();

        ProcessClimbing();
    }

    //****************************************************************************************************
    private void CheckForGround()
    {
        // Assume we are falling
        isPlayerGrounded.value = false;

        // Create a list of colliders
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckTransform.position, groundedRadius.value, groundLayerMask);

        // If there is a collider that isn't ourselves then we are grounded
        for (int i = 0; i < colliders.Length; i++)
            if (colliders[i].gameObject != gameObject)
                isPlayerGrounded.value = true;
    }

    //****************************************************************************************************
    private void ProcessHorizontalMovement()
    {
        // Calculate the amount to move the player based on the players movement speed
        Vector2 moveVect = new Vector2(userInputObj.getUserInputRawHorizontal() * playerMoveSpeed.value, playerRb2D.velocity.y);

        // Smooth the movement and apply it
        playerRb2D.velocity = Vector2.SmoothDamp(playerRb2D.velocity, moveVect, ref horizontalVelocity, playerMoveSmoothFactor.value / 100f);
    }

    //****************************************************************************************************
    private void ProcessJump()
    {
        // If the user requested jump
        if (userInputObj.getUserInputBoolJump() && isPlayerGrounded.value && !isPlayerOnLadder.value)
            playerRb2D.AddForce(Vector2.up * playerJumpVelocity.value * 100f);

        // If the player is falling
        if (playerRb2D.velocity.y < 0)
        {
            ApplyNewGravity();
        }
        // If the player is not falling but has let go of jump
        else if (playerRb2D.velocity.y > 0 && !userInputObj.getUserInputBoolJump())
        {
            ApplyNewGravity();
        }
    }

    //****************************************************************************************************
    private void ApplyNewGravity()
    {
        if (isPlayerOnLadder.value)
            return;

        playerRb2D.velocity += Vector2.up * Physics2D.gravity.y * (playerGravityMultiplier.value - 1f) * Time.fixedDeltaTime;
    }

    //****************************************************************************************************
    private void ProcessClimbing()
    {
        // Raycast upwards to detect a ladder
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, playerLadderDetectDistance.value, ladderLayerMask);

        // If we have detected a ladder
        if (hitInfo.collider != null)
        {
            // If the user presses up
            if (userInputObj.getUserInputRawVertical() > 0)
                isPlayerOnLadder.value = true;
        }
        else
        {
            isPlayerOnLadder.value = false;
        }

        // If we are climbing
        if (isPlayerOnLadder.value)
        {
            // Move the player based on the players movement speed
            Vector2 moveVect = new Vector2(playerRb2D.velocity.x, userInputObj.getUserInputRawVertical() * (playerMoveSpeed.value / 2f));

            // Smooth the movement and apply it
            playerRb2D.velocity = Vector2.SmoothDamp(playerRb2D.velocity, moveVect, ref verticalVelocity, playerMoveSmoothFactor.value / 100f);

            // Turn off gravity
            playerRb2D.gravityScale = 0;
        }
        else
        {
            // Reset gravity
            playerRb2D.gravityScale = defaultGravityScale.value;
        }
    }

    //****************************************************************************************************
    private void ProcessFlip()
    {
        float currentDirection = userInputObj.getUserInputRawHorizontal();

        // If user has requested right and we are facing left
        if (currentDirection > 0 && !isPlayerFacingRight.value)
            FlipPlayer();
        // If user has requested left and we are facing right
        else if (currentDirection < 0 && isPlayerFacingRight.value)
            FlipPlayer();
    }

    //****************************************************************************************************
    private void FlipPlayer()
    {
        // Set direction to opposite
        isPlayerFacingRight.value = !isPlayerFacingRight.value;

        // Rotate the player
        transform.Rotate(0f, 180f, 0f);
    }
}
