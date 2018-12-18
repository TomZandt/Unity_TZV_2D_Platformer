using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StateMachine_Action_Patrol : StateMachine_Action
{
    private bool isWaypoint1 = false;

    //****************************************************************************************************
    public override void Act(StateMachine_Controller _controller)
    {
        Patrol(_controller);
    }

    private void Patrol(StateMachine_Controller _controller)
    {
        if (Vector2.Distance(_controller.transform.position, _controller.waypoints[0].position) > 0.1f && isWaypoint1)
        {
            _controller.transform.position = Vector2.MoveTowards(_controller.transform.position, _controller.waypoints[0].position, _controller.enemyStats.enemyMoveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            isWaypoint1 = false;

            if (Vector2.Distance(_controller.transform.position, _controller.waypoints[1].position) > 0.1f)
            {
                _controller.transform.position = Vector2.MoveTowards(_controller.transform.position, _controller.waypoints[1].position, _controller.enemyStats.enemyMoveSpeed * Time.fixedDeltaTime);
            }
            else
            {
                isWaypoint1 = true;
                return;
            }
        }
    }
}
