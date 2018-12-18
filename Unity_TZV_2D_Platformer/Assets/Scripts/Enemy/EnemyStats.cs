using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStats : ScriptableObject
{
    public float enemyMoveSpeed = 2f;
    public float enemyChaseSpeed = 4f;
    public float playerDetectRadius = 5f;
    public float playerChasePadding = 2f;
}
