using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StateMachine_Decision_PlayerActive", menuName = "TZV/ScriptableObjects/StateMachine_Decision_PlayerActive", order = 1)]
public class StateMachine_Decision_PlayerActive : StateMachine_Decision
{
    public override bool Decide(StateMachine_Controller _controller)
    {
        return _controller.playerGO.activeSelf;
    }
}
