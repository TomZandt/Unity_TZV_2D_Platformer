using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool isJumping = false;
    bool isCrouching = false;

    //******************************************************************************
    // Update is called once per frame
    //******************************************************************************
    void Update ()
    {
        // Get horizontal movement from user
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        // If there is an animator assigned
        if (animator != null)
        {
            // Set 'Speed' parameter for player animation
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }

        // If the user pressed jump
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;

            // If there is an animator assigned
            if (animator != null)
            {
                // Set 'isJumping' parameter for player animation
                animator.SetBool("isJumping", true);
            }
        }

        // If the user pressed crouch
        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
        }
    }

    //******************************************************************************
    // Called every physics step
    //******************************************************************************
    void FixedUpdate ()
    {
        // Move the character with consistant speed using delta time
        controller.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
        isJumping = false;
    }

    //******************************************************************************
    // Public function for on landing event
    //******************************************************************************
    public void OnLanding ()
    {
        // If there is an animator assigned
        if (animator != null)
        {
            // Set 'isJumping' parameter for player animation
            animator.SetBool("isJumping", false);
        }
    }

    //******************************************************************************
    // Public function for on landing event
    //******************************************************************************
    public void OnCrouching (bool bisCrouching)
    {
        // If there is an animator assigned
        if (animator != null)
        {
            // Set 'isCrouching' parameter for player animation
            animator.SetBool("isCrouching", bisCrouching);
        }
    }
}
