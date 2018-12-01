using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_UserInput : MonoBehaviour
{
    private float userInputHorizontal = 0f;

    //****************************************************************************************************
    private void Update()
    {
        // Get the horizontal axis input from the user
        userInputHorizontal = Input.GetAxisRaw("Horizontal");
    }

    //****************************************************************************************************
    public float getUserInputHorizontal ()
    {
        return userInputHorizontal;
    }
}
