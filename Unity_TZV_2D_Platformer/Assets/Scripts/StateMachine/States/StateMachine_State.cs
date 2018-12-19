using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StateMachine_State : ScriptableObject
{
    public StateMachine_Action[] actions;
    public StateMachine_Switch[] switchs;

    //****************************************************************************************************
    public void UpdateState(StateMachine_Controller _controller)
    {
        DoAction(_controller);
        DoSwitchState(_controller);
    }

    //****************************************************************************************************
    private void DoAction(StateMachine_Controller _controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(_controller);
        }
    }

    //****************************************************************************************************
    private void DoSwitchState(StateMachine_Controller _controller)
    {
        for (int i = 0; i < switchs.Length; i++)
        {
            bool switchAllowed = switchs[i].decision.Decide(_controller);

            if (switchAllowed)
            {
                _controller.SwitchToState(switchs[i].stateIfTrue);
            }
            else
            {
                _controller.SwitchToState(switchs[i].stateIfFalse);
            }
        }
    }
}
