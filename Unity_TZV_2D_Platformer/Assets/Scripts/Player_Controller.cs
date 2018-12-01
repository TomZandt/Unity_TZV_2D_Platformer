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

    private Player_UserInput userInputObj;
    private Rigidbody2D playerRb2D;

    private Vector2 velocity = Vector2.zero; // Used for smoothDamp

    private const float groundedRadius = 0.02f;
    private bool isGrounded = false;

    private bool isFacingRight = true;

    //****************************************************************************************************
    private void Start()
    {
        // Assign the user input object
        userInputObj = GetComponent<Player_UserInput>();

        // Assign the rigid body object
        playerRb2D = GetComponent<Rigidbody2D>();
        playerRb2D.bodyType = RigidbodyType2D.Dynamic;
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
        playerRb2D.velocity = Vector2.SmoothDamp(playerRb2D.velocity, moveVect, ref velocity, playerMoveSmoothFactor / 100);
    }

    //****************************************************************************************************
    private void ProcessJump()
    {
        // If the user requested jump
        if (userInputObj.getUserInputBoolJump() && isGrounded)
            playerRb2D.AddForce(Vector2.up * playerJumpVelocity * 100f);

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
        playerRb2D.velocity += Vector2.up * Physics2D.gravity.y * (playerGravityMultiplier - 1) * Time.fixedDeltaTime;
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
