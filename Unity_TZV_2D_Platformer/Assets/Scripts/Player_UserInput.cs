using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_UserInput : MonoBehaviour
{
    private float userInputRawHorizontal = 0f;
    private float userInputRawVertical = 0f;
    private bool userInputBoolJump = false;

    //****************************************************************************************************
    private void Update()
    {
        // Get the horizontal axis input from the user
        userInputRawHorizontal = Input.GetAxisRaw("Horizontal");

        // Get the vertical axis input from the user
        userInputRawVertical = Input.GetAxisRaw("Vertical");

        // Get the jump input from the user
        userInputBoolJump = Input.GetButton("Jump");
    }

    //****************************************************************************************************
    public float getUserInputRawHorizontal ()
    {
        return userInputRawHorizontal;
    }

    //****************************************************************************************************
    public float getUserInputRawVertical()
    {
        return userInputRawVertical;
    }

    //****************************************************************************************************
    public bool getUserInputBoolJump()
    {
        return userInputBoolJump;
    }
}
