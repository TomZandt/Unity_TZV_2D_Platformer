using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[CreateAssetMenu]
public class StateMachine_Action_Chase : StateMachine_Action
{
    private Vector2 refVel;
    private Seeker seeker;
    private Path path;
    private bool hasPathEnded = false;
    private int currentWaypoint = 0;

    private float relativeDistanceFromPlayer;

    //****************************************************************************************************
    public override void Act(StateMachine_Controller _controller)
    {
        /*
 Transform self = _controller.transform;
 Transform target = _controller.chaseTarget.transform;

 relativeDistanceFromPlayer = self.position.x - target.position.x;

 // Target is to my left
 if (relativeDistanceFromPlayer > _controller.enemyStats.playerChasePadding)
 {
     _controller.transform.position = Vector2.MoveTowards(_controller.transform.position, _controller.chaseTarget.position, _controller.enemyStats.enemyChaseSpeed * Time.deltaTime);
 }
 // Target is to my right
 else if (relativeDistanceFromPlayer < -_controller.enemyStats.playerChasePadding)
 {
     _controller.transform.position = Vector2.MoveTowards(_controller.transform.position, _controller.chaseTarget.position, _controller.enemyStats.enemyChaseSpeed * Time.deltaTime);
 }*/
        if (_controller.aiSetter.getCanSearch() != true)
        {
            _controller.aiSetter.setCanSearch(true);
        }
    }

    //****************************************************************************************************
    public override void FixedAct(StateMachine_Controller _controller)
    {/*
        // Find path to target
        FindPath(_controller);

        // Make sure we have a path to follow
        if (path == null)
            return;

        // If we have reached the final waypoint
        if (currentWaypoint >= path.vectorPath.Count)
        {
            // We have no path
            if (hasPathEnded)
                return;

            // Update bool
            Debug.Log("Reached end of path");
            hasPathEnded = true;
            return;
        }

        // Update bool, we still have waypoints
        hasPathEnded = false;

        // Get direction to next waypoint (target - enemy) vector
        Vector2 direction = path.vectorPath[currentWaypoint] - _controller.transform.position;

        // Normalise direction
        direction = direction.normalized;

        // Create velocity
        direction *= _controller.enemyStats.enemyChaseSpeed * Time.fixedDeltaTime;

        // Move
        _controller.rb.AddForce(direction, _controller.enemyStats.enemyForceMode);


        // If we are close enough to the waypoint
        if (Vector2.Distance(_controller.transform.position, path.vectorPath[currentWaypoint]) < _controller.enemyStats.waypointCompleteDistance)
        {
            currentWaypoint++;
            return;
        }
        */
    }

    //****************************************************************************************************
    private void FindPath(StateMachine_Controller _controller)
    {
        if (seeker == null)
        {
            Debug.Log("No Seeker");
            seeker = _controller.GetComponent<Seeker>();
        }

        // Start a new path to the target and return result to OnPathComplete
        seeker.StartPath(_controller.transform.position, _controller.chaseTarget.position, OnPathComplete);
    }

    //****************************************************************************************************
    private void OnPathComplete(Path _p)
    {
        if (!_p.error)
        {
            path = _p;
            currentWaypoint = 0;
        }
        else
        {
            Debug.Log(_p.errorLog);
        }
    }
}
