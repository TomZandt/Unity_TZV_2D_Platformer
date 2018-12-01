using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] [Header("The move speed of the player")]                   private float playerMoveSpeed = 10f;
    [SerializeField] [Header("The jump velocity of the player")]                private float playerJumpVelocity = 5f;
    [SerializeField] [Header("The movement smoothing factor")]                  private float playerMoveSmoothFactor = 5f;
    [SerializeField] [Header("The player gravity multiplier")]                  private float playerGravityMultiplier = 3f;
    [SerializeField] [Header("The transform position to check if grounded")]    private Transform groundCheckTransform;
    [SerializeField] [Header("The layer mask denoting what is ground")]         private LayerMask groundLayerMask;
    [SerializeField] [Header("The layer mask denoting what is a ladder")]       private LayerMask ladderLayerMask;
    [SerializeField] [Header("The distance the player can detect a ladder")]    private float ladderDetectDistance = 2f;

    private Player_UserInput userInputObj;
    private Rigidbody2D playerRb2D;

    private const float defaultGravityScale = 1f; // Constant gravity scale for climbing etc

    private Vector2 horizontalVelocity = Vector2.zero;  // Used for smoothDamp
    private Vector2 verticalVelocity = Vector2.zero;    // Used for smoothDamp

    private const float groundedRadius = 0.02f;
    private bool isGrounded = false;
    private bool isFacingRight = true;
    private bool isClimbing = false;

    //****************************************************************************************************
    private void Start()
    {
        // Assign the user input object
        userInputObj = GetComponent<Player_UserInput>();

        // Assign the rigid body object
        playerRb2D = GetComponent<Rigidbody2D>();
        playerRb2D.bodyType = RigidbodyType2D.Dynamic;
        playerRb2D.gravityScale = defaultGravityScale;
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
        isGrounded = false;

        // Create a list of colliders
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckTransform.position, groundedRadius, groundLayerMask);

        // If there is a collider that isn't ourselves then we are grounded
        for (int i = 0; i < colliders.Length; i++)
            if (colliders[i].gameObject != gameObject)
                isGrounded = true;
    }

    //****************************************************************************************************
    private void ProcessHorizontalMovement()
    {
        // Calculate the amount to move the player based on the players movement speed
        Vector2 moveVect = new Vector2(userInputObj.getUserInputRawHorizontal() * playerMoveSpeed, playerRb2D.velocity.y);

        // Smooth the movement and apply it
        playerRb2D.velocity = Vector2.SmoothDamp(playerRb2D.velocity, moveVect, ref horizontalVelocity, playerMoveSmoothFactor / 100f);
    }

    //****************************************************************************************************
    private void ProcessJump()
    {
        // If the user requested jump
        if (userInputObj.getUserInputBoolJump() && isGrounded && !isClimbing)
        {
            playerRb2D.AddForce(Vector2.up * playerJumpVelocity * 100f);
        }
        
        // If the player is falling
        if (playerRb2D.velocity.y < 0)
            ApplyNewGravity();

        // If the player is not falling but has let go of jump
        else if (playerRb2D.velocity.y > 0 && !userInputObj.getUserInputBoolJump())
            ApplyNewGravity();
    }

    //****************************************************************************************************
    private void ApplyNewGravity()
    {
        if (isClimbing)
            return;

        playerRb2D.velocity += Vector2.up * Physics2D.gravity.y * (playerGravityMultiplier - 1f) * Time.fixedDeltaTime;
    }

    //****************************************************************************************************
    private void ProcessClimbing()
    {
        // Raycast upwards to detect a ladder
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, ladderDetectDistance, ladderLayerMask);

        // If we have detected a ladder
        if (hitInfo.collider != null)
        {
            // If the user presses up
            if (userInputObj.getUserInputRawVertical() > 0)
                isClimbing = true;
        }
        else
        {
            isClimbing = false;
        }

        // If we are climbing
        if (isClimbing)
        {
            // Move the player based on the players movement speed
            Vector2 moveVect = new Vector2(playerRb2D.velocity.x, userInputObj.getUserInputRawVertical() * (playerMoveSpeed / 2f));

            // Smooth the movement and apply it
            playerRb2D.velocity = Vector2.SmoothDamp(playerRb2D.velocity, moveVect, ref verticalVelocity, playerMoveSmoothFactor / 100f);

            // Turn off gravity
            playerRb2D.gravityScale = 0;
        }
        else
        {
            // Reset gravity
            playerRb2D.gravityScale = defaultGravityScale;
        }
    }

        //****************************************************************************************************
        private void ProcessFlip()
    {
        float currentDirection = userInputObj.getUserInputRawHorizontal();

        // If user has requested right and we are facing left
        if (currentDirection > 0 && !isFacingRight)
            FlipPlayer();
        // If user has requested left and we are facing right
        else if (currentDirection < 0 && isFacingRight)
            FlipPlayer();
    }

    //****************************************************************************************************
    private void FlipPlayer()
    {
        // Set direction to opposite
        isFacingRight = !isFacingRight;

        // Rotate the player
        transform.Rotate(0f, 180f, 0f);
    }
}
