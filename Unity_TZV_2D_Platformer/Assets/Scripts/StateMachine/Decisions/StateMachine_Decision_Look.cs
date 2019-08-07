using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StateMachine_Decision_Look", menuName = "TZV/ScriptableObjects/StateMachine_Decision_Look", order = 1)]
public class StateMachine_Decision_Look : StateMachine_Decision
{
    //****************************************************************************************************
    public override bool Decide(StateMachine_Controller _controller)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_controller.transform.position, _controller.enemyStats.playerDetectRadius, LayerMask.GetMask("Player"));

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                _controller.aiTarget = colliders[i].transform.position;
                return true;
            }
        }

        return false;
    }
}
