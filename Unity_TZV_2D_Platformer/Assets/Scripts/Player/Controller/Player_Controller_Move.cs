using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller_Move : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    private Rigidbody2D rb;
    private float userInputRawHorizontal;
    private Vector2 horizontalVelocity = Vector2.zero;  // Used for smoothDamp

    //****************************************************************************************************
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats.playerFacingRight = true;
    }

    //****************************************************************************************************
    private void Update()
    {
        userInputRawHorizontal = Input.GetAxisRaw("Horizontal");
        UpdateOrientation();
    }

    //****************************************************************************************************
    private void FixedUpdate()
    {
        // Calculate the amount to move the player based on the players movement speed
        Vector2 moveVect = new Vector2(userInputRawHorizontal * playerStats.playerMoveSpeed, rb.velocity.y);

        // Smooth the movement and apply it
        rb.velocity = Vector2.SmoothDamp(rb.velocity, moveVect, ref horizontalVelocity, playerStats.playerMoveSmoothFactor / 100f);
    }

    //****************************************************************************************************
    private void UpdateOrientation()
    {
        float currentDirection = userInputRawHorizontal;

        // If user has requested right and we are facing left
        if (currentDirection > 0 && !playerStats.playerFacingRight)
        {
            FlipPlayer();
        }
        // If user has requested left and we are facing right
        else if (currentDirection < 0 && playerStats.playerFacingRight)
        {
            FlipPlayer();
        }
    }

    //****************************************************************************************************
    private void FlipPlayer()
    {
        // Set direction to opposite
        playerStats.playerFacingRight = !playerStats.playerFacingRight;

        // Rotate the player
        transform.Rotate(0f, 180f, 0f);
    }
}
