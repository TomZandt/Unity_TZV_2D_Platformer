using UnityEngine;

[CreateAssetMenu(fileName = "V3_SO_Input", menuName = "Scriptable Objects/V3_SO_Input", order = 1)]
public class V3_SO_Input : ScriptableObject
{
    public float horizontal = 0f;
    public float freelookHorizontal = 0f;
    public float freelookVertical = 0f;
    public bool isJumpPressed = false;
    public bool isJumpHeld = false;
    public bool isCrouchPressed = false;
    public bool isCrouchHeld = false;
    public bool isWallGrabHeld = false;
    public bool isDashPressed = false;
    public bool isPausePressed = false;

    //****************************************************************************************************
    public void Reset()
    {
        horizontal = 0f;
        freelookHorizontal = 0f;
        freelookVertical = 0f;
        isJumpPressed = false;
        isJumpHeld = false;
        isCrouchPressed = false;
        isCrouchHeld = false;
        isWallGrabHeld = false;
        isDashPressed = false;
        isPausePressed = false;
    }
}


