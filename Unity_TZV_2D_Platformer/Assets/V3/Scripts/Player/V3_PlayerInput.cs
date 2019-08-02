///****************************************************************************************************
// Reference to https://www.youtube.com/watch?v=83xn7QYpS_s&list=PLX2vGYjWbI0REfhDHPpdIBjjrzDHDP-xT&index=6
//****************************************************************************************************

using UnityEngine;

[DefaultExecutionOrder(-100)] // Ensure script runs before other player scripts to prevent input lag

public class V3_PlayerInput : MonoBehaviour
{
    public float horizontal;
    public bool isJumpPressed;
    public bool isJumpHeld;
    public bool isCrouchPressed;
    public bool isCrouchHeld;

    private bool readyToClear; // Used in FixedUpdate to ensure code gets current input

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

        readyToClear = false;
    }

    //****************************************************************************************************
    private void ProcessInput()
    {
        horizontal += Input.GetAxis("Horizontal");
        isJumpPressed = Input.GetButtonDown("Jump");
        isJumpHeld = Input.GetButton("Jump");
        isCrouchPressed = Input.GetButtonDown("Crouch");
        isCrouchHeld = Input.GetButton("Crouch");
    }
}
