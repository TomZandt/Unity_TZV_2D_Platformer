using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StateMachine_Action_Patrol : StateMachine_Action
{
    private bool isWaypoint1 = false;

    private Vector2 nextWaypoint;
    private bool findNext = true;

    //****************************************************************************************************
    public override void Act(StateMachine_Controller _controller)
    {
        if(_controller.useRandomWithinWaypoints)
        {
            randomPatrol(_controller, _controller.waypoints[0].position, _controller.waypoints[1].position);
        }
        else
        {
            simplePatrol(_controller, _controller.waypoints[0].position, _controller.waypoints[1].position);
        }
    }

    //****************************************************************************************************
    public override void FixedAct(StateMachine_Controller _controller)
    {
        
    }

    //****************************************************************************************************
    private void simplePatrol(StateMachine_Controller _controller, Vector2 _waypoint1, Vector2 _waypoint2)
    {
        // If we are to far from waypoint and is waypoint 1
        if (Vector2.Distance(_controller.transform.position, _waypoint1) > _controller.enemyStats.waypointCompleteDistance && isWaypoint1)
        {
            // Move to waypoint 1
            _controller.transform.position = Vector2.MoveTowards(_controller.transform.position, _waypoint1, _controller.enemyStats.enemyMoveSpeed * Time.deltaTime);
        }
        else
        {
            // Must be waypoint 2
            isWaypoint1 = false;

            // If we are too far from waypoint 2
            if (Vector2.Distance(_controller.transform.position, _waypoint2) > _controller.enemyStats.waypointCompleteDistance)
            {
                // Move to waypoint 2
                _controller.transform.position = Vector2.MoveTowards(_controller.transform.position, _waypoint2, _controller.enemyStats.enemyMoveSpeed * Time.deltaTime);
            }
            else
            {
                // Must be waypoint 1
                isWaypoint1 = true;
                return;
            }
        }
    }

    //****************************************************************************************************
    private void randomPatrol(StateMachine_Controller _controller, Vector2 _waypoint1, Vector2 _waypoint2)
    {
        // If the waypoint is meant to be random
        if (findNext)
        {
            // Pick a random point between the 2 waypoints
            float randomX = Random.Range(_waypoint1.x, _waypoint2.x);

            // Update the vector to the new waypoint
            nextWaypoint = new Vector2(randomX, _waypoint1.y);
        }

        // If we are too far from the waypoint
        if (Vector2.Distance(_controller.transform.position, nextWaypoint) > _controller.enemyStats.waypointCompleteDistance)
        {
            // Move towards the waypoint
            _controller.transform.position = Vector2.MoveTowards(_controller.transform.position, nextWaypoint, _controller.enemyStats.enemyMoveSpeed * Time.deltaTime);

            // Look for new waypoint
            findNext = false;
        }
        else
        {
            // Look for new waypoint
            findNext = true;
        }
    }
}
