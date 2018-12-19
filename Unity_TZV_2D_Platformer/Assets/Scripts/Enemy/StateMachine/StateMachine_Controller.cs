using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_Controller : MonoBehaviour
{
    public StateMachine_State currentState;
    public StateMachine_State remainState;
    public EnemyStats enemyStats;
    public Transform detectionTransform;
    public Transform[] waypoints;
    [HideInInspector] public int nextWaypoint;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public float countdown = 0f;

    //****************************************************************************************************
    private void Update()
    {
        currentState.UpdateState(this);
    }

    //****************************************************************************************************
    public void SwitchToState(StateMachine_State _state)
    {
        if (remainState != _state)
        {
            currentState = _state;

            OnExitState();
        }
    }

    //****************************************************************************************************
    public bool checkCountdown(float _duration)
    {
        countdown += Time.deltaTime;

        return (countdown >= _duration);
    }

    //****************************************************************************************************
    private void OnExitState ()
    {
        countdown = 0;
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
