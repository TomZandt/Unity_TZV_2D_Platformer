using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine_Action : ScriptableObject
{
    //****************************************************************************************************
    public abstract void Act(StateMachine_Controller _controller);

    //****************************************************************************************************
    public abstract void FixedAct(StateMachine_Controller _controller);
}
