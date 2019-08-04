//****************************************************************************************************
// Reference to https://www.youtube.com/watch?v=83xn7QYpS_s&list=PLX2vGYjWbI0REfhDHPpdIBjjrzDHDP-xT&index=6
// Reference to https://www.youtube.com/watch?v=Pzd8NhcRzVo&t=39s
// Reference to http://guavaman.com/projects/rewired/docs/QuickStart.html
//****************************************************************************************************

using UnityEngine;
using Rewired;

[DefaultExecutionOrder(-1000)] // Ensure script runs before other player scripts to prevent input lag

public class V3_PlayerInput : MonoBehaviour
{
    [HideInInspector] public float horizontal;
    [HideInInspector] public float freelookHorizontal;
    [HideInInspector] public float freelookVertical;
    [HideInInspector] public bool isJumpPressed;
    [HideInInspector] public bool isJumpHeld;
    [HideInInspector] public bool isCrouchPressed;
    [HideInInspector] public bool isCrouchHeld;
    [HideInInspector] public bool isWallGrabHeld;
    [HideInInspector] public bool isDashPressed;

    private bool readyToClear;  // Used in FixedUpdate to ensure code gets current input
    private Player player;      // Rewired player

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
        freelookHorizontal = Mathf.Clamp(freelookHorizontal, -1f, 1f);
        freelookVertical = Mathf.Clamp(freelookVertical, -1f, 1f);
    }

    //****************************************************************************************************
    private void FixedUpdate()
    {
        // Set a flag to allow inputs to be cleared out during the next Update(). Ensures code uses currrent input
        readyToClear = true;
    }

    //****************************************************************************************************
    public void ClearInput()
    {
        // If FixedUpdate hasnt happened yet
        if (!readyToClear)
        {
            return;
        }

        // Clear all input
        horizontal = 0f;
        freelookHorizontal = 0f;
        freelookVertical = 0f;
        isJumpPressed = false;
        isJumpHeld = false;
        isCrouchPressed = false;
        isCrouchHeld = false;
        isWallGrabHeld = false;
        isDashPressed = false;

        // Not ready
        readyToClear = false;
    }

    //****************************************************************************************************
    private void ProcessInput()
    {
        // USing accumulation to accumulate inputs

        // Axis
        horizontal += player.GetAxis("Move Horizontal");
        freelookHorizontal += player.GetAxis("Freelook Horizontal");
        freelookVertical += player.GetAxis("Freelook Vertical");

        // Buttons
        isJumpPressed = isJumpPressed || player.GetButtonDown("Jump");
        isJumpHeld = isJumpHeld || player.GetButton("Jump");

        isCrouchPressed = isCrouchPressed || player.GetButtonDown("Crouch");
        isCrouchHeld = isCrouchHeld || player.GetButton("Crouch");

        isWallGrabHeld = isWallGrabHeld || player.GetButton("Wall Grab");

        isDashPressed = isDashPressed || player.GetButtonDown("Dash");
    }
}
