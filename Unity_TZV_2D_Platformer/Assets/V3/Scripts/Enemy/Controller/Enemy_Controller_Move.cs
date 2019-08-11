using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller_Move : MonoBehaviour
{
    public EnemyStats enemyStats;
    
    private Rigidbody2D rb;
    private Vector3 Direction;
    private Vector2 horizontalVelocity = Vector2.zero;  // Used for smoothDamp
    private EnemyPathfinder pathFinder;

    //****************************************************************************************************
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pathFinder = GetComponent<EnemyPathfinder>();
        enemyStats.isEnemyFacingRight = true;
    }

    //****************************************************************************************************
    private void Update()
    {
        Direction = pathFinder.getDirection();
        UpdateOrientation();
    }

    //****************************************************************************************************
    private void FixedUpdate()
    {
        Move();

    }

    //****************************************************************************************************
    private void Move()
    {
        // Calculate the amount to move the player based on the players movement speed
        Vector2 moveVect = new Vector2(Direction.x * enemyStats.enemyMoveSpeed, rb.velocity.y);

        // Smooth the movement and apply it
        rb.velocity = Vector2.SmoothDamp(rb.velocity, moveVect, ref horizontalVelocity, enemyStats.enemyMoveSmoothFactor / 100f);
    }

    //****************************************************************************************************
    private void UpdateOrientation()
    {
        float currentDirection = Direction.x;

        // If user has requested right and we are facing left
        if (currentDirection > 0 && !enemyStats.isEnemyFacingRight)
        {
            FlipPlayer();
        }
        // If user has requested left and we are facing right
        else if (currentDirection < 0 && enemyStats.isEnemyFacingRight)
        {
            FlipPlayer();
        }
    }

    //****************************************************************************************************
    private void FlipPlayer()
    {
        // Set direction to opposite
        enemyStats.isEnemyFacingRight = !enemyStats.isEnemyFacingRight;

        // Rotate the player
        transform.Rotate(0f, 180f, 0f);
    }
}
