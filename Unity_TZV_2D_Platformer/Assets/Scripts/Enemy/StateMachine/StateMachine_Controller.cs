using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_Controller : MonoBehaviour
{
    public StateMachine_State currentState;

    public EnemyStats enemyStats;

    public Transform detectionTransform;

    public Transform[] waypoints;
    [HideInInspector] public int nextWaypoint;


    //****************************************************************************************************
    private void Update()
    {
        currentState.UpdateState(this);
    }

    //****************************************************************************************************
    // Debug Gizmos
    //****************************************************************************************************
    private void OnDrawGizmos()
    {
        if (currentState != null && detectionTransform != null)
        {
            Gizmos.color = currentState.gizmoColourForState;

            Gizmos.DrawWireSphere(detectionTransform.position, enemyStats.playerDetectRadius);
        }
    }
}
