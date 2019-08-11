using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "TZV/ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    [Header("Health")]
    public int playerMaxHealth = 5;
    public int playerHealth = 5;

    [Header("Attack")]
    public bool playerCanFire = true;
    public float playerAttackRate = 0.5f;
    public float playerAttackDamage = 5f;

    [Header("Movement")]
    [HideInInspector] public bool playerFacingRight = true;
    public float playerMoveSmoothFactor = 5f;
    public float playerMoveSpeed = 10f;

    [Header("Gravity")]
    public float playerDefaultGravity = 1f;
    public float playerGravityMultiplier = 5f;

    [Header("Ground")]
    public LayerMask playerGroundLayerMask;
    public bool playerGrounded = false;
    public float playerGroundedRadius = 0.05f;

    [Header("Jumping")]
    public ForceMode2D playerJumpForceMode;
    public bool playerPressedJump = false;
    public bool playerHoldingJump = false;
    public float playerJumpVelocity = 13.5f;

    [Header("Ladders")]
    public LayerMask playerLadderLayerMask;
    public bool playerOnLadder = false;
    public float playerLadderDetectDistance = 2f;
    public float playerLadderSmoothFactor = 5f;
    public float playerLadderSpeed = 6f;
}
