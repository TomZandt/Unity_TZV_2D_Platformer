using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] [Header("The move speed of the player")]       private float playerMoveSpeed = 10f;
    [SerializeField] [Header("The jump velocity of the player")]    private float playerJumpVelocity = 5f;
    [SerializeField] [Header("The movement smoothing factor")]      private float playerMoveSmoothFactor = 5f;
    //private bool facingRight = true;

    private Player_UserInput userInputObj;
    private Rigidbody2D playerRb2D;
    private Vector2 moveVect = Vector2.zero;
    private Vector2 velocity = Vector2.zero;

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
        // Calculate the amount to move the player based on the players movement speed
        moveVect = new Vector2(userInputObj.getUserInputRawHorizontal() * playerMoveSpeed, playerRb2D.velocity.y);

        // Smooth the movement and apply it
        moveVect = Vector2.SmoothDamp(playerRb2D.velocity, moveVect, ref velocity, playerMoveSmoothFactor / 100);
    }

    //****************************************************************************************************
    private void FixedUpdate()
    {
        // Calculate the amount to move the player based on the players movement speed
        moveVect = new Vector2(userInputObj.getUserInputRawHorizontal() * playerMoveSpeed, playerRb2D.velocity.y);

        // Smooth the movement and apply it
        playerRb2D.velocity = Vector2.SmoothDamp(playerRb2D.velocity, moveVect, ref velocity, playerMoveSmoothFactor / 100);

        // If the user requested jump
        if (userInputObj.getUserInputBoolJump())
        {
            playerRb2D.AddForce(Vector2.up * playerJumpVelocity * 100f);
        }
    }
}
