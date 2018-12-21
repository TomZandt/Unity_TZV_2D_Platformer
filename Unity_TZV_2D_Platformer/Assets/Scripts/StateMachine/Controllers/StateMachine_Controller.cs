using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class StateMachine_Controller : MonoBehaviour
{
    public Rigidbody2D rb;
    public StateMachine_State currentState;
    public StateMachine_State remainState;
    public EnemyStats enemyStats;
    public Transform detectionTransform;
    public Transform[] waypoints;
    public bool useRandomWithinWaypoints = true;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public float attackTime = 0f;

    //****************************************************************************************************
    private void Update()
    {
        currentState.UpdateState(this);
    }

    //****************************************************************************************************
    private void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    //****************************************************************************************************
    public void SwitchToState(StateMachine_State _state)
    {
        if (remainState != _state)
        {
            currentState = _state;
        }
    }
}
