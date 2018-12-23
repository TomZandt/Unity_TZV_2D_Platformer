using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[CreateAssetMenu]
public class StateMachine_Action_Chase : StateMachine_Action
{
    //****************************************************************************************************
    public override void Act(StateMachine_Controller _controller)
    {
        _controller.aiTarget = _controller.playerGO.transform.position;
    }

    //****************************************************************************************************
    public override void FixedAct(StateMachine_Controller _controller)
    {

    }
}
