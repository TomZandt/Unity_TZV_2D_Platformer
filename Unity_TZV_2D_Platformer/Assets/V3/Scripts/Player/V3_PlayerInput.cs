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
    public V3_SO_Input inputSO;

    private bool readyToClear = true;   // Used in FixedUpdate to ensure code gets current input
    private Player player;              // Rewired player

    //****************************************************************************************************
    private void Awake()
    {
        if (inputSO == null)
        {
            Debug.LogError("No SO Found - TZV V3_PlayerInput");
        }

        ClearInput();

        // Assign the rewired player
        player = ReInput.players.GetPlayer(0); // 0 default for first player
    }

    //****************************************************************************************************
    private void Update()
    {
        // Clear any existing input values
        ClearInput();      

        // Process inputs
        ProcessInput();

        // Scale input to -1 to 1
        inputSO.horizontal = Mathf.Clamp(inputSO.horizontal, -1f, 1f);
        inputSO.freelookHorizontal = Mathf.Clamp(inputSO.freelookHorizontal, -1f, 1f);
        inputSO.freelookVertical = Mathf.Clamp(inputSO.freelookVertical, -1f, 1f);
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
        inputSO.horizontal = 0f;
        inputSO.freelookHorizontal = 0f;
        inputSO.freelookVertical = 0f;
        inputSO.isJumpPressed = false;
        inputSO.isJumpHeld = false;
        inputSO.isCrouchPressed = false;
        inputSO.isCrouchHeld = false;
        inputSO.isWallGrabHeld = false;
        inputSO.isDashPressed = false;
        inputSO.isPausePressed = false;

        // Not ready
        readyToClear = false;
    }

    //****************************************************************************************************
    private void ProcessInput()
    {
        // Using accumulation to accumulate inputs

        // Axis
        inputSO.horizontal += player.GetAxis("Move Horizontal");
        inputSO.freelookHorizontal += player.GetAxis("Freelook Horizontal");
        inputSO.freelookVertical += player.GetAxis("Freelook Vertical");

        // Buttons
        inputSO.isJumpPressed = inputSO.isJumpPressed || player.GetButtonDown("Jump");
        inputSO.isJumpHeld = inputSO.isJumpHeld || player.GetButton("Jump");

        inputSO.isCrouchPressed = inputSO.isCrouchPressed || player.GetButtonDown("Crouch");
        inputSO.isCrouchHeld = inputSO.isCrouchHeld || player.GetButton("Crouch");

        inputSO.isWallGrabHeld = inputSO.isWallGrabHeld || player.GetButton("Wall Grab");

        inputSO.isDashPressed = inputSO.isDashPressed || player.GetButtonDown("Dash");


        inputSO.isPausePressed = player.GetButtonDown("Pause");
    }
}
