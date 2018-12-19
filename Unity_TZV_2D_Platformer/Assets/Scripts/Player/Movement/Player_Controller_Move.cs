using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller_Move : MonoBehaviour
{
    [SerializeField] private FloatVariable playerMoveSpeed;
    [SerializeField] private FloatVariable playerMoveSmoothFactor;
    [SerializeField] private BoolVariable isPlayerFacingRight;

    private Rigidbody2D rb;
    private float userInputRawHorizontal;
    private Vector2 horizontalVelocity = Vector2.zero;  // Used for smoothDamp

    //****************************************************************************************************
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        Vector2 moveVect = new Vector2(userInputRawHorizontal * playerMoveSpeed.value, rb.velocity.y);

        // Smooth the movement and apply it
        rb.velocity = Vector2.SmoothDamp(rb.velocity, moveVect, ref horizontalVelocity, playerMoveSmoothFactor.value / 100f);
    }

    //****************************************************************************************************
    private void UpdateOrientation()
    {
        float currentDirection = userInputRawHorizontal;

        // If user has requested right and we are facing left
        if (currentDirection > 0 && !isPlayerFacingRight.value)
        {
            FlipPlayer();
        }
        // If user has requested left and we are facing right
        else if (currentDirection < 0 && isPlayerFacingRight.value)
        {
            FlipPlayer();
        }
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
