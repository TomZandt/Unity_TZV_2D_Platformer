using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller_Gravity : MonoBehaviour
{
    [SerializeField] private FloatVariable playerGravityMultiplier;
    [SerializeField] private FloatVariable defaultGravityScale;
    [SerializeField] private BoolVariable isPlayerHoldingJump;
    [SerializeField] private BoolVariable isPlayerOnLadder;

    private Rigidbody2D rb;

    //****************************************************************************************************
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //****************************************************************************************************
    private void FixedUpdate()
    {
        if (isPlayerOnLadder.value)
        {
            rb.gravityScale = 0;
        }
        else
        {
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = playerGravityMultiplier.value;
            }
            else if (rb.velocity.y > 0 && !isPlayerHoldingJump.value)
            {
                rb.gravityScale = playerGravityMultiplier.value;
            }
            else
            {
                rb.gravityScale = defaultGravityScale.value;
            }
        }
    }
}
