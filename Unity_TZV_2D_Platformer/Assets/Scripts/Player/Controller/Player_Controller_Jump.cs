using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller_Jump : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameEvent jumpGameEvent;

    private Rigidbody2D rb;

    //****************************************************************************************************
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //****************************************************************************************************
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            playerStats.playerPressedJump = true;
            jumpGameEvent.Raise();
        }

        if (Input.GetButton("Jump"))
        {
            playerStats.playerHoldingJump = true;
        }
        else
        {
            playerStats.playerHoldingJump = false;
        }
    }

    //****************************************************************************************************
    private void FixedUpdate()
    {
        if (playerStats.playerPressedJump)
        {
            if (playerStats.playerGrounded && !playerStats.playerOnLadder)
            {
                rb.AddForce(Vector2.up * playerStats.playerJumpVelocity, playerStats.playerJumpForceMode);
                playerStats.playerPressedJump = false;
            }
        }
    }
}
