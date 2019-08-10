using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller_SetIsGrounded : MonoBehaviour
{
    public PlayerStats playerStats;
    public Transform groundCheckTransform;

    //****************************************************************************************************
    void Update()
    {
        // Assume we are falling
        playerStats.playerGrounded = false;

        // Create a list of colliders with the ground layer mask
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckTransform.position, playerStats.playerGroundedRadius, playerStats.playerGroundLayerMask);

        // If there is a collider that isn't ourselves then we are grounded
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                playerStats.playerGrounded = true;
            }
        }
    }
}
