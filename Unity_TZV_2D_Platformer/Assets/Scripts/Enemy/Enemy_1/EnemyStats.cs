using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStats : ScriptableObject
{
    public float enemyMoveSpeed = 2f;
    public float enemyChaseSpeed = 4f;
    public ForceMode2D enemyForceMode = ForceMode2D.Impulse;
    public float waypointCompleteDistance = 1f;
    public float enemyAttackRadius = 2f;
    public float enemyAttackRate = 1f;
    public float playerDetectRadius = 5f;
    public float playerChasePadding = 2f;
    public float enemyHealth = 8f;
}
