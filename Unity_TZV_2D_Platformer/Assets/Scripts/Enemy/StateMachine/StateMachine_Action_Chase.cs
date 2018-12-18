using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StateMachine_Action_Chase : StateMachine_Action
{
    Vector2 refVel;

    float relativeDistanceFromPlayer;

    //****************************************************************************************************
    public override void Act(StateMachine_Controller _controller)
    {
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
        }
        else
        {
            _controller.transform.position = _controller.transform.position;
        }
    }
}
