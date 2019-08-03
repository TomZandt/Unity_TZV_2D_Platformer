//****************************************************************************************************
// Reference to https://www.youtube.com/watch?v=83xn7QYpS_s&list=PLX2vGYjWbI0REfhDHPpdIBjjrzDHDP-xT&index=6
// Reference to https://www.youtube.com/watch?v=Pzd8NhcRzVo&t=39s
// Reference to http://guavaman.com/projects/rewired/docs/QuickStart.html
//****************************************************************************************************

using UnityEngine;
using Rewired;

[DefaultExecutionOrder(-100)] // Ensure script runs before other player scripts to prevent input lag

public class V3_PlayerInput : MonoBehaviour
{
    public float horizontal;
    public bool isJumpPressed;
    public bool isJumpHeld;
    public bool isCrouchPressed;
    public bool isCrouchHeld;
    public bool isWallGrabHeld;
    public bool isDashPressed;

    private bool readyToClear; // Used in FixedUpdate to ensure code gets current input
    private Player player; // Rewired player

    //****************************************************************************************************
    private void Awake()
    {
        // Assign the rewired player
        player = ReInput.players.GetPlayer(0); // 0 default for first player
    }

    //****************************************************************************************************
    private void Update()
    {
        // Clear any existing input values
        ClearInput();

        // Exit if is game over
        if (V3_GameManager.IsGameOver())
        {
            return;
        }

        // Process inputs
        ProcessInput();

        // Scale input to -1 to 1
        horizontal = Mathf.Clamp(horizontal, -1f, 1f);
    }

    //****************************************************************************************************
    private void FixedUpdate()
    {
        readyToClear = true;
    }

    //****************************************************************************************************
    private void ClearInput()
    {
        // If FixedUpdate hasnt happened yet
        if (!readyToClear)
        {
            return;
        }

        // Clear all input
        horizontal = 0f;
        isJumpPressed = false;
        isJumpHeld = false;
        isCrouchPressed = false;
        isCrouchHeld = false;
        isWallGrabHeld = false;
        isDashPressed = false;

        readyToClear = false;
    }

    //****************************************************************************************************
    private void ProcessInput()
    {
        horizontal += player.GetAxis("Move Horizontal");
        isJumpPressed = player.GetButtonDown("Jump");
        isJumpHeld = player.GetButton("Jump");
        isCrouchPressed = player.GetButtonDown("Crouch");
        isCrouchHeld = player.GetButton("Crouch");
        isWallGrabHeld = player.GetButton("Wall Grab");
        isDashPressed = player.GetButtonDown("Dash");
    }
}
