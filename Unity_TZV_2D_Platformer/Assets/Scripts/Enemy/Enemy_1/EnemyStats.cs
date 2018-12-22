using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStats : ScriptableObject
{
    public float enemyMoveSpeed = 2f;
    public float waypointCompleteDistance = 3f;
    public float enemyAttackRadius = 2f;
    public float enemyAttackRate = 1f;
    public float playerDetectRadius = 5f;
    public float playerChasePadding = 2f;
    public float enemyHealth = 8f;
    public float enemyMoveSmoothFactor = 5f;
    public bool isEnemyFacingRight = true;
    public bool allowedToChase = false;
    public Vector3 target;
}
