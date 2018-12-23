using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStats : ScriptableObject
{
    [Header("Health")]
    public float enemyMaxHealth = 8f;
    public float enemyHealth = 8f;

    [Header("Attack")]
    public float enemyAttackRadius = 2f;
    public float enemyAttackRate = 1f;
    public int enemyAttackDamage = 1;

    [Header("Movement")]
    [ReadOnly] public bool isEnemyFacingRight = true;
    public float enemyMoveSmoothFactor = 5f;
    public float enemyMoveSpeed = 2f;

    [Header("Path Finding")]
    public Vector3 target;
    public float waypointCompleteDistance = 3f;

    [Header("State Machine")]
    public float playerDetectRadius = 5f;
}
