using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StateMachine_State : ScriptableObject
{
    public StateMachine_Action[] actions;

    public Color gizmoColourForState = Color.white; // Debug

    //****************************************************************************************************
    public void UpdateState(StateMachine_Controller _controller)
    {
        DoActions(_controller);
    }

    //****************************************************************************************************
    private void DoActions(StateMachine_Controller _controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(_controller);
        }
    }
}
