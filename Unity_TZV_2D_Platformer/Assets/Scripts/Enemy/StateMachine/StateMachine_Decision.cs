using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine_Decision : ScriptableObject
{
    //****************************************************************************************************
    public abstract bool Decide(StateMachine_Controller _controller);
}
