using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller_Ladder : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameEvent ladderGameEvent;

    private Rigidbody2D rb;
    private float userInputRawVertical;
    private Vector2 verticalVelocity = Vector2.zero;    // Used for smoothDamp

    //****************************************************************************************************
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //****************************************************************************************************
    private void Update()
    {
        userInputRawVertical = Input.GetAxisRaw("Vertical");

        // Raycast upwards to detect a ladder
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, playerStats.playerLadderDetectDistance, playerStats.playerLadderLayerMask);

        // If we have detected a ladder
        if (hitInfo.collider != null)
        {
            // If the user presses up
            if (userInputRawVertical > 0)
            {
                playerStats.playerOnLadder = true;
                ladderGameEvent.Raise();
            }
        }
        else
        {
            playerStats.playerOnLadder = false;
        }
    }

    //****************************************************************************************************
    private void FixedUpdate()
    {
        // If we are climbing
        if (playerStats.playerOnLadder)
        {
            // Move the player based on the players movement speed
            Vector2 moveVect = new Vector2(rb.velocity.x, userInputRawVertical * playerStats.playerLadderSpeed);

            // Smooth the movement and apply it
            rb.velocity = Vector2.SmoothDamp(rb.velocity, moveVect, ref verticalVelocity, playerStats.playerLadderSmoothFactor / 100f);
        }
    }
}
