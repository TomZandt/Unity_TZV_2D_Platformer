using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

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
    [HideInInspector] public MyAIPathSetter aiSetter;

    //****************************************************************************************************
    private void Start()
    {
        aiSetter = GetComponent<MyAIPathSetter>();
    }

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
