//****************************************************************************************************
// Reference to https://www.youtube.com/watch?v=83xn7QYpS_s&list=PLX2vGYjWbI0REfhDHPpdIBjjrzDHDP-xT&index=6
// Reference to http://dotween.demigiant.com/getstarted.php
// Reference to https://www.youtube.com/watch?v=STyY26a_dPY
//****************************************************************************************************

using UnityEngine;
using System.Collections;
using DG.Tweening;

public class V3_PlayerMovement : MonoBehaviour
{
    public V3_SO_Player playerSO;
    public V3_SO_Input inputSO;

    // Objects
    public SpriteRenderer sprite;
    private BoxCollider2D bodyCollider;
    private Rigidbody2D rigidBody;

    private float jumpTime;
    private float coyoteTime;
    private float playerHeight;
    private float originalXScale;
    private int direction = 1;              // Player facing direction

    private Vector2 colliderStandSize;      // Size of the standing collider
    private Vector2 colliderStandOffset;    // Offset of the standing collider
    private Vector2 colliderCrouchSize;     // Size of the crouching collider
    private Vector2 colliderCrouchOffset;   // Offset of the crouching collider

    private const float smallAmount = 0.05f; // A small amount used for hanging position

    //****************************************************************************************************
    private void Awake()
    {
        if(playerSO == null || inputSO == null)
        {
            Debug.LogError("No SO Found - TZV V3_PlayerMovement");
        }
    }

    //****************************************************************************************************
    private void Start()
    {
        // Get Ref
        bodyCollider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();

        // Save originals
        originalXScale = transform.localScale.x;
        playerHeight = bodyCollider.size.y;
        colliderStandSize = bodyCollider.size;
        colliderStandOffset = bodyCollider.offset;

        // Calc new sollider size and offset for crouch
        colliderCrouchSize = new Vector2(bodyCollider.size.x, bodyCollider.size.y / playerSO.crouchSizeDivider);
        colliderCrouchOffset = new Vector2(bodyCollider.offset.x, bodyCollider.offset.y / playerSO.crouchSizeDivider);

        playerSO.isFacingRight = true;
        playerSO.canDash = true;
    }

    //****************************************************************************************************
    private void FixedUpdate()
    {
        PhysicsCheck();
        GroundMovement();
        AirMovement();
        CheckForDash();
    }

    //****************************************************************************************************
    private void PhysicsCheck()
    {
        // Assume we are grounded and standing
        playerSO.isOnGround = false;
        playerSO.isHeadBlocked = false;

        // Raycast for left and right feet
        RaycastHit2D leftCheck = Raycast(new Vector2(-playerSO.footOffset, 0f), Vector2.down, playerSO.groundDistance);
        RaycastHit2D rightCheck = Raycast(new Vector2(playerSO.footOffset, 0f), Vector2.down, playerSO.groundDistance);

        // If either ray hit the ground, the player is on the ground
        if (leftCheck || rightCheck)
        {
            playerSO.isOnGround = true;
        }

        //Cast the ray to check above the player's head
        RaycastHit2D headCheck = Raycast(new Vector2(0f, bodyCollider.size.y), Vector2.up, playerSO.headClearance);

        //If that ray hits, the player's head is blocked
        if (headCheck)
        {
            playerSO.isHeadBlocked = true;
        }

        // Determine the direction of the wall grab attempt
        Vector2 grabDir = new Vector2(direction, 0f);

        // Cast three rays to look for a wall grab
        RaycastHit2D blockedCheck = Raycast(new Vector2(playerSO.footOffset * direction, playerHeight), grabDir, playerSO.grabDistance);
        RaycastHit2D ledgeCheck = Raycast(new Vector2(playerSO.reachOffset * direction, playerHeight), Vector2.down, playerSO.grabDistance);
        RaycastHit2D wallCheck = Raycast(new Vector2(playerSO.footOffset * direction, playerSO.eyeHeight), grabDir, playerSO.grabDistance);

        // If the player is off the ground AND is not hanging AND is falling AND found a ledge AND found a wall AND the grab is NOT blocked...
        if (!playerSO.isOnGround && !playerSO.isHanging && rigidBody.velocity.y < 0f && ledgeCheck && wallCheck && !blockedCheck && inputSO.isWallGrabHeld)
        {
            // ...we have a ledge grab. Record the current position...
            Vector3 pos = transform.position;

            // ...move the distance to the wall (minus a small amount)...
            pos.x += (wallCheck.distance - smallAmount) * direction;

            // ...move the player down to grab onto the ledge...
            pos.y -= ledgeCheck.distance;

            // ...apply this position to the platform...
            transform.position = pos;

            // ...set the rigidbody to static...
            rigidBody.bodyType = RigidbodyType2D.Static;

            // ...finally, set isHanging to true
            playerSO.isHanging = true;
        }
    }

    //****************************************************************************************************
    private void GroundMovement()
    {
        // If currently hanging, the player can't move to exit
        if (playerSO.isHanging)
            return;

        // Handle crouching input. If holding the crouch button but not crouching, crouch
        if (inputSO.isCrouchHeld && !playerSO.isCrouching && playerSO.isOnGround)
        {
            Crouch();
        }
        //Otherwise, if not holding crouch but currently crouching, stand up
        else if (!inputSO.isCrouchHeld && playerSO.isCrouching)
        {
            StandUp();
        }
        //Otherwise, if crouching and no longer on the ground, stand up
        else if (!playerSO.isOnGround && playerSO.isCrouching)
        {
            StandUp();
        }

        // Calculate the desired velocity based on inputs
        float xVelocity = playerSO.speed * inputSO.horizontal;

        // If the sign of the velocity and direction don't match, flip the character
        if (xVelocity * direction < 0f)
        {
            FlipCharacterDirection();
        }

        // If the player is crouching, reduce the velocity
        if (playerSO.isCrouching)
        {
            xVelocity /= playerSO.crouchSpeedDivider;

            sprite.transform.localScale = new Vector3(1f, 0.9f, 1f);
            sprite.transform.localPosition = new Vector3(0f, 0.45f, 0f);
        }
        else
        {
            if (sprite.transform.localScale != new Vector3(1f, 1.8f, 1f) || sprite.transform.localPosition != new Vector3(0f, 0.9f, 0f))
            {
                sprite.transform.localScale = new Vector3(1f, 1.8f, 1f);
                sprite.transform.localPosition = new Vector3(0f, 0.9f, 0f);
            }
        }

        // Apply X Velocity
        if (playerSO.useAcceleration)
        {
            // Apply the desired velocity & Acceleration
            rigidBody.velocity = new Vector2(Mathf.MoveTowards(rigidBody.velocity.x, xVelocity, playerSO.acceleration * Time.deltaTime), rigidBody.velocity.y);
        }
        else
        {
            // Apply the desired velocity 
            rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);
        }

        // If the player is on the ground, extend the coyote time window
        if (playerSO.isOnGround)
        {
            coyoteTime = Time.time + playerSO.coyoteDuration;
        }
    }

    //****************************************************************************************************
    private void AirMovement()
    {
        // If the player is currently hanging...
        if (playerSO.isHanging)
        {
            // If dash is pressed
            CheckForDash();

            // If player lets go
            if (!inputSO.isWallGrabHeld)
            {
                // ...let go
                playerSO.isHanging = false;

                // ...set the rigidbody to dynamic and exit
                rigidBody.bodyType = RigidbodyType2D.Dynamic;

                return;
            }

            // If jump is pressed...
            if (inputSO.isJumpPressed)
            {
                // ...let go
                playerSO.isHanging = false;

                // ...set the rigidbody to dynamic and apply a jump force
                rigidBody.bodyType = RigidbodyType2D.Dynamic;
                rigidBody.AddForce(new Vector2(0f, playerSO.hangingJumpForce), ForceMode2D.Impulse);

                //...and exit
                return;
            }
        }

        // If the jump key is pressed AND the player isn't already jumping AND EITHER the player is on the ground or within the coyote time window...
        if (inputSO.isJumpPressed && !playerSO.isJumping && (playerSO.isOnGround || coyoteTime > Time.time) && !playerSO.isHeadBlocked)
        {
            // ...check to see if crouching AND not blocked. If so
            if (playerSO.isCrouching && !playerSO.isHeadBlocked)
            {
                // ...stand up and apply a crouching jump boost
                StandUp();

                rigidBody.AddForce(new Vector2(0f, playerSO.crouchJumpBoost), ForceMode2D.Impulse);
            }

            // ...The player is no longer on the ground and is jumping
            playerSO.isOnGround = false;
            playerSO.isJumping = true;

            // ...record the time the player will stop being able to boost their jump
            jumpTime = Time.time + playerSO.jumpHoldDuration;

            // ...add the jump force to the rigidbody
            rigidBody.AddForce(new Vector2(0f, playerSO.jumpForce), ForceMode2D.Impulse);

            // ...and tell the Audio Manager to play the jump audio
            V3_AudioManager.PlayJumpAudio();
        }
        // Otherwise, if currently within the jump time window
        else if (playerSO.isJumping)
        {
            // ...and the jump button is held, apply an incremental force to the rigidbody
            if (inputSO.isJumpHeld)
            {
                rigidBody.AddForce(new Vector2(0f, playerSO.jumpHoldForce), ForceMode2D.Impulse);
            }

            // ...and if jump time is past, set isJumping to false
            if (jumpTime <= Time.time)
            {
                playerSO.isJumping = false;
            }
        }

        // If player is falling to fast, reduce the Y velocity to the max
        if (rigidBody.velocity.y < playerSO.maxFallSpeed)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, playerSO.maxFallSpeed);
        }
    }

    //****************************************************************************************************
    private void FlipCharacterDirection()
    {
        // Turn the character by flipping the direction
        playerSO.isFacingRight = !playerSO.isFacingRight;
        direction *= -1;

        // Record the current scale
        Vector3 scale = transform.localScale;

        // Set the X scale to be the original times the direction
        scale.x = originalXScale * direction;

        // Apply the new scale
        transform.localScale = scale;
    }

    //****************************************************************************************************
    private void Crouch()
    {
        // The player is crouching
        playerSO.isCrouching = true;

        // Apply the crouching collider size and offset
        bodyCollider.size = colliderCrouchSize;
        bodyCollider.offset = colliderCrouchOffset;
    }

    //****************************************************************************************************
    private void StandUp()
    {
        // If the player's head is blocked, they can't stand so exit
        if (playerSO.isHeadBlocked)
            return;

        // The player isn't crouching
        playerSO.isCrouching = false;

        // Apply the standing collider size and offset
        bodyCollider.size = colliderStandSize;
        bodyCollider.offset = colliderStandOffset;
    }

    //****************************************************************************************************
    private void CheckForDash()
    {
        // If dash is pressed
        if (inputSO.isDashPressed && playerSO.canDash)
        {
            if (playerSO.isHanging)
            {
                playerSO.isHanging = false;
                rigidBody.bodyType = RigidbodyType2D.Dynamic;
                FlipCharacterDirection();
            }

            playerSO.canDash = false;
            DOVirtual.Float(14f, 0f, 0.4f, RigidBodyDrag);
            rigidBody.AddForce(new Vector2(direction * playerSO.dashForce * 100f, 0f), ForceMode2D.Force);
            StartCoroutine(DashTime());
        }
    }

    //****************************************************************************************************
    private void RigidBodyDrag(float _drag)
    {
        rigidBody.drag = _drag;
    }

    //****************************************************************************************************
    private IEnumerator DashTime()
    {
        yield return new WaitForSeconds(playerSO.dashWaitTime);
        playerSO.canDash = true;
    }

    //****************************************************************************************************
    //These two Raycast methods wrap the Physics2D.Raycast() and provide some extra functionality
    private RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length)
    {
        // Call the overloaded Raycast() method using the ground layermask and return the results
        return Raycast(offset, rayDirection, length, playerSO.groundLayer);
    }

    //****************************************************************************************************
    private RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask mask)
    {
        // Record the player's position
        Vector2 pos = transform.position;

        // Send out the desired raycasr and record the result
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, mask);

        // If we want to show debug raycasts in the scene...
        if (playerSO.drawDebugRaycasts)
        {
            // ...determine the color based on if the raycast hit...
            Color color = hit ? Color.red : Color.green;
            // ...and draw the ray in the scene view
            Debug.DrawRay(pos + offset, rayDirection * length, color);
        }

        // Return the results of the raycast
        return hit;
    }
}
