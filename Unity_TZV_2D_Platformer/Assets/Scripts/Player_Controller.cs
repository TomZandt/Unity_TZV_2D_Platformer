using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] [Header("The move speed of the player")] private float playerMoveSpeed = 10f;
    //private bool facingRight = true;

    private Player_UserInput userInputObj;
    private Rigidbody2D playerRb2D;
    private Vector2 horizontalMoveVect;

    //****************************************************************************************************
    private void Start()
    {
        // Assign the user input object
        userInputObj = GetComponent<Player_UserInput>();

        // Assign the rigid body object
        playerRb2D = GetComponent<Rigidbody2D>();
        playerRb2D.bodyType = RigidbodyType2D.Dynamic;
        playerRb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        playerRb2D.interpolation = RigidbodyInterpolation2D.Interpolate;
        playerRb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    //****************************************************************************************************
    private void Update()
    {
        // Calculate the amount to move the player based on the players movement speed
        horizontalMoveVect = new Vector2(userInputObj.getUserInputHorizontal() * playerMoveSpeed, 0);
    }

    //****************************************************************************************************
    private void FixedUpdate()
    {
        // Move the players rigid body
        playerRb2D.MovePosition(playerRb2D.position + horizontalMoveVect * Time.fixedDeltaTime);
    }
}
