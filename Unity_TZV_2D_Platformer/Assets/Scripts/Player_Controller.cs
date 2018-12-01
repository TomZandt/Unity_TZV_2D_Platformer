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

    private Vector2 moveVect = Vector2.zero; // Used for smoothDamp
    private Vector2 velocity = Vector2.zero; // Used for smoothDamp

    private const float groundedRadius = 0.02f;
    private bool isGrounded = false;

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
    private void FixedUpdate()
    {
        // Assume we are falling
        isGrounded = false;

        // Create a list of colliders
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckTransform.position, groundedRadius, groundLayerMask);

        // If there is a collider that isn't ourselves then we are grounded
        for (int i = 0; i < colliders.Length; i++)
            if (colliders[i].gameObject != gameObject)
                isGrounded = true;

        // Calculate the amount to move the player based on the players movement speed
        moveVect = new Vector2(userInputObj.getUserInputRawHorizontal() * playerMoveSpeed, playerRb2D.velocity.y);

        // Smooth the movement and apply it
        playerRb2D.velocity = Vector2.SmoothDamp(playerRb2D.velocity, moveVect, ref velocity, playerMoveSmoothFactor / 100);

        // If the user requested jump
        if (userInputObj.getUserInputBoolJump() && isGrounded)
            playerRb2D.AddForce(Vector2.up * playerJumpVelocity * 100f);

        // Add increased gravity and short jump control
        if (playerRb2D.velocity.y < 0 || playerRb2D.velocity.y > 0 && !userInputObj.getUserInputBoolJump())
            playerRb2D.velocity += Vector2.up * Physics2D.gravity.y * (playerGravityMultiplier - 1) * Time.fixedDeltaTime;
    }
}
