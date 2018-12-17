using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller_Jump : MonoBehaviour
{
    [SerializeField] private FloatVariable playerJumpVelocity;

    [SerializeField] private BoolVariable playerPressedJump;
    [SerializeField] private BoolVariable isPlayerGrounded;
    [SerializeField] private BoolVariable isPlayerOnLadder;
    [SerializeField] private BoolVariable isPlayerHoldingJump;

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
            playerPressedJump.value = true;
            jumpGameEvent.Raise();
        }

        if (Input.GetButton("Jump"))
        {
            isPlayerHoldingJump.value = true;
        }
        else
        {
            isPlayerHoldingJump.value = false;
        }
    }

    //****************************************************************************************************
    private void FixedUpdate()
    {
        if (playerPressedJump.value)
        {
            if (isPlayerGrounded.value && !isPlayerOnLadder.value)
            {
                rb.AddForce(Vector2.up * playerJumpVelocity.value, ForceMode2D.Impulse);
                playerPressedJump.value = false;
            }
        }
    }
}
