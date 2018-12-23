using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class StateMachine_Controller : MonoBehaviour
{
    public StateMachine_State currentState;
    public StateMachine_State remainState;
    public EnemyStats enemyStats;
    public Transform detectionTransform;
    public Transform[] waypoints;
    public bool useRandomWithinWaypoints = true;

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public float attackTime = 0f;
    [HideInInspector] public Transform player;

    [SerializeField] public GameEvent enemyAttackGameEvent;

    //****************************************************************************************************
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

    //****************************************************************************************************
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyStats.enemyAttackRadius * 2);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, enemyStats.playerDetectRadius);
    }
}
