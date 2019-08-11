using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[CreateAssetMenu(fileName = "StateMachine_Action_Chase", menuName = "TZV/ScriptableObjects/StateMachine_Action_Chase", order = 1)]
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
