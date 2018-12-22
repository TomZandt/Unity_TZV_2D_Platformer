using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller_Gravity : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    private Rigidbody2D rb;

    //****************************************************************************************************
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //****************************************************************************************************
    private void FixedUpdate()
    {
        if (playerStats.playerOnLadder)
        {
            rb.gravityScale = 0;
        }
        else
        {
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = playerStats.playerGravityMultiplier;
            }
            else if (rb.velocity.y > 0 && !playerStats.playerHoldingJump)
            {
                rb.gravityScale = playerStats.playerGravityMultiplier;
            }
            else
            {
                rb.gravityScale = playerStats.playerDefaultGravity;
            }
        }
    }
}
