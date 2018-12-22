using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StateMachine_Decision_PlayerActive : StateMachine_Decision
{
    public override bool Decide(StateMachine_Controller _controller)
    {
        return _controller.player.gameObject.activeSelf;
    }
}
