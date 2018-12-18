using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateMachine_Switch
{
    public StateMachine_Decision decision;
    public StateMachine_State stateIfTrue;
    public StateMachine_State stateIfFalse;
}
